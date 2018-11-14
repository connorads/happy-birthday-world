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

![Swagger UI](https://i.postimg.cc/CLRY9mD4/happy-birthday-world.gif)

## Environments

Happy Birthday World is deployed on Google Cloud Platform*  
It's got the Swagger UI running so you can test it quite easily 👍  

Environment | Repo | Storage Status
----------- | --------------  | --------------  
Local | ¯\\\_(ツ)\_/¯ | PostgreSQL or in-memory mode
[Staging](http://happy-birthday-world.jx-staging.35.234.144.66.nip.io/) | [razorbow-staging](https://github.com/connorads/environment-razorbow-staging) |Connected to Staging Cloud SQL instance
[Production](http://happy-birthday-world.jx-production.35.234.144.66.nip.io/) | [razorbow-production](https://github.com/connorads/environment-razorbow-production) |Using in-memory mode (ephemeral)

_* for as long as my credits last ... please don't hammer it 😅_  


## Technologies

A non-exhaustive list of technologies used.

* ASP.NET Core 2.1
* Swagger / Swashbuckle.AspNetCore
* xUnit
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

### Google Cloud Platform

![GKE Services](http://i68.tinypic.com/29xgfms.png)
![Cloud SQL](http://i64.tinypic.com/2rm39lf.png)

## Assumptions

Here are some assumptions that need to be validated by the "Product Owner" 👀

* Leap-year babies are born on February 29th. We currently assume a birthday of February 28th on non-leap years for leap-year babies. But some "leapers" celebrate on March 1st [*[Wikipedia]*](https://bit.ly/2ENDhFe)
* Dates of Birth in the future are invalid. We perform validation to ensure that the Date of Births entered on the API are not in the future because otherwise it's difficult to define the expected behaviour.
* Name can be a maximum of [50 characters](https://stackoverflow.com/a/15474655/4319653).

## Todo

* ~~Deploy Cloud SQL proxy sidecar in Staging~~
* ~~Tidy up secrets: got a mix/repeat of DB secrets with env and volumes~~
* ~~Upgrade Swagger page to show example date format using C# XML comments~~
* Connect Cloud SQL in "Production" (perhaps use [PodPreset](https://kubernetes.io/docs/concepts/workloads/pods/podpreset/) to [define Cloud SQL Instances](https://cloud.google.com/sql/docs/mysql/sql-proxy#instances-options) in [deployment yaml command argument](https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/))
* Automatically run tests as part of the build ([*dotnet docker best practices*](https://github.com/dotnet/dotnet-docker/blob/master/samples/dotnetapp/dotnet-docker-unit-testing.md))
* Add Database Health check
* Return validation error message for when a DOB that is in the future is submitted (it already returns a bad request but without a reason)
* Fix JX Pull request preview environment deployments
* Many more ...