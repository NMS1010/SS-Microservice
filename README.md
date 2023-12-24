**What is SS-Microservice .NET 6?**
----------------
My name is Nguyen Minh Son, this is my personal project, providing in-depth knowledge about building microservices using [.NET 6](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial) framework and variety of tools. One of the goals, was to create a microservice architecture solution, that you shall be able to run anywhere. 

**What technologies will be used?**
----------------
Back-end
- [RESTful API](https://www.restapitutorial.com) implementation with [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
- Validation with [FluentValidation](https://fluentvalidation.net/)
- SQL databases (MySQL) with [Entity Framework Core 6](https://learn.microsoft.com/en-us/ef/core/)
- Other patterns designed for microservices: CQRS, API Gateway, Service Registry, Saga Orchestration
- Other patterns: UnitOfWork, Generic Repository, Specification
- [JWT](https://jwt.io), authentication, authorization
- Communication via websockets using [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/?view=aspnetcore-2.1)
- [CQRS](https://martinfowler.com/bliki/CQRS.html), Commands, Queries & Events handlers with [Mediator](https://github.com/jbogard/MediatR)
- Using [RabbitMQ](https://www.rabbitmq.com) as a message queue with [Masstransit](https://masstransit.io/)
- Dealing with asynchronous requests, and Sagas Orchestration using [Masstransit](https://masstransit.io/)
- Internal communication with [RestEase](https://github.com/canton7/RestEase), [gRPC](https://learn.microsoft.com/vi-vn/aspnet/core/grpc/?view=aspnetcore-6.0)
- API Gateway with [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html)
- Service discovery with [Consul](https://www.consul.io)
- Monitoring with [OpenTelemetry](https://opentelemetry.io/), [Grafana](https://grafana.com), [Prometheus](https://prometheus.io)
- Tracing with [Jaeger](https://www.jaegertracing.io)
- Logging with [Serilog](https://serilog.net) and [ELK stack](https://www.elastic.co/elk-stack)
- Building [Docker](https://www.docker.com) images, managing containers, networks and registries
- Defining [Docker compose](https://docs.docker.com/compose) stacks

Front-end
- Building UI with [ReactJS Vite](https://vitejs.dev/guide/), [Ant Design](https://ant.design/), [TailwindCSS](https://tailwindcss.com/) + [SCSS](https://sass-lang.com/), [React Router 6](https://reactrouter.com/en/main)
- API communication with [Axios](https://github.com/axios/axios)
- Server State Management with [React Query](https://tanstack.com/query/v3/)
- Building [Docker](https://www.docker.com) images

**How to start the solution?**
----------------

1. You need to have the [Docker Desktop](https://www.docker.com/products/docker-desktop/) with Docker compose on your machine
2. Clone this repository to your machine
3. Open terminal in this repository folder
4. Run the following commands in order:

This command to build Infrastructure for application:
```docker
docker-compose -f ./compose/docker-compose-infrastructure.yml up -d
```

Then, run this command to build services for Backend:
```docker
docker-compose up -d
```

Finally, run this command to build Frontend:
```docker
docker-compose -f ./docker-compose-ui.yml up -d
```
5. All done, you can access
   - Frontend: http://localhost:5173
   - Backend:
     - Gateway: http://localhost:5201
     - Consul Service Discovery: http://localhost:8500
     - Jaeger Tracing: http://localhost:16686
     - Kibana UI for logging: http://localhost:5601 (need to create indexes correctly with the service name in `docker-compose.yml` file)
     - Grafana + Prometheus for monitoring: http://localhost:3000/d/KdDACDp4z
     - RabbitMQ for message queue (user/password: guest/guest): http://localhost:15672 
