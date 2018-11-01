# Happy Birthday World

Get a personalised birthday countdown and happy birthday message 🎂

* Save/update name and date of birth  
  * `PUT /hello/Connor { "dateOfBirth" : "1997-08-04" }`
  * `204 No Content`
* Return birthday related message
  * `GET /hello/Connor`
    * When Connor's Birthday is in 7 days:
      * `{ "message" : "Hello, Connor! Your birthday is in 7 days" }`
    * When Connor's Birthday is today:
      * `{ "message" : "Hello, Connor! Happy Birthday!" }`

## Environments

Running on my Google Cloud Platform, please don't hammer it 😅  
It's got the Swagger UI running so you can test it quite easily 👍  
Probably best to avoid the Staging environment for now 😬

Environment | Storage Status
----------- |  --------------  
Local | Works with PostgreSQL and in-memory mode
[Staging](http://happy-birthday-world.jx-staging.35.234.144.66.nip.io/) | Not working, needs Cloud SQL proxy sidecar
[Production](http://happy-birthday-world.jx-production.35.234.144.66.nip.io/) | Works but in-memory (ephemeral)

## Technologies

A non-exhaustive list of technologies used.

* ASP.NET Core 2.1
* PostgreSQL / Google Cloud SQL
* Kubernetes / GKE
* NGINX
* ChartMuseum (Helm)
* Docker Registry
* Jenkins
* Monocular
* Nexus
* Fabric8


### Jenkins-X

[Jenkins X](https://jenkins-x.io/) is a CI/CD solution for modern cloud applications on Kubernetes.  
Push to master and the Jenkins-X platform will build, version, tag, and release the app to Staging.

If you want to promote a version of your app to production:  
`jx promote --version 0.0.9 --env production`  

## Assumptions

Here are some assumptions that need to be validated by the "Product Owner" 👀

* Leap-year babies are born on February 29th. We currently assume a birthday of February 28th on non-leap years for leap-year babies. But some "leapers" celebrate on March 1st [*[Wikipedia]*](https://bit.ly/2ENDhFe)
* Dates of Birth in the future are invalid. We perform validation to ensure that the Date of Births entered on the API are not in the future because otherwise it's difficult to define the expected behaviour.

## Todo

* Update this README!
* Deploy Cloud SQL proxy sidecar in Staging
* Tidy up secrets: got a mix/repeat of DB secrets with env and volumes
* Setup database in Production environment
* Add Database Health check
* Upgrade Swagger page to show example date format using C# XML comments
* Return validation error message for when a DOB that is in the future is submitted
* JX Pull request preview environment deployment doesn't work 😥
* Automatically run tests as part of the build [*(dotnet docker best practices)*](https://github.com/dotnet/dotnet-docker/blob/master/samples/dotnetapp/dotnet-docker-unit-testing.md)
* Many more ...