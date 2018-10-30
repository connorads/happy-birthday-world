pipeline {
    agent {
        label "jenkins-jx-base"
    }
    environment {
      ORG               = 'connorads'
      APP_NAME          = 'happy-birthday-world'
      CHARTMUSEUM_CREDS = credentials('jenkins-x-chartmuseum')
    }
    stages {
      stage('CI Build and push snapshot') {
        when {
          branch 'PR-*'
        }
        environment {
          PREVIEW_VERSION = "0.0.0-SNAPSHOT-$BRANCH_NAME-$BUILD_NUMBER"
          PREVIEW_NAMESPACE = "$APP_NAME-$BRANCH_NAME".toLowerCase()
          HELM_RELEASE = "$PREVIEW_NAMESPACE".toLowerCase()
        }
        steps {
          container('jx-base') {
            sh "docker build -t $DOCKER_REGISTRY/$ORG/$APP_NAME:$PREVIEW_VERSION ./src"
            sh "docker push $DOCKER_REGISTRY/$ORG/$APP_NAME:$PREVIEW_VERSION"
          }

          dir ('./charts/preview') {
           container('jx-base') {
             sh "make preview"
             sh "jx preview --app $APP_NAME --dir ../.."
           }
          }
        }
      }
      stage('Build Release') {
        when {
          branch 'master'
        }
        steps {
          container('jx-base') {
            // ensure we're not on a detached head
            sh "git checkout master"
            // until we switch to the new kubernetes / jenkins credential implementation use git credentials store
            sh "git config --global credential.helper store"
            // so we can retrieve the version in later steps
            sh "echo \$(jx-release-version) > VERSION"
          }
          dir ('./charts/happy-birthday-world') {
            container('jx-base') {
              sh "jx step git credentials"
              sh "make tag"
            }
          }
          container('jx-base') {
            sh "docker build -t $DOCKER_REGISTRY/$ORG/$APP_NAME:\$(cat VERSION) ./src"
            sh "docker push $DOCKER_REGISTRY/$ORG/$APP_NAME:\$(cat VERSION)"
          }
        }
      }
      stage('Promote to Environments') {
        when {
          branch 'master'
        }
        steps {
          dir ('./charts/happy-birthday-world') {
            container('jx-base') {
              sh 'jx step changelog --version v\$(cat ../../VERSION)'

              // release the helm chart
              sh 'jx step helm release'

              // promote through all 'Auto' promotion Environments
              sh 'jx promote -b --all-auto --timeout 1h --version \$(cat ../../VERSION)'
            }
          }
        }
      }
    }
    post {
        always {
            cleanWs()
        }
    }
  }
