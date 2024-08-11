DI 是用来将对象和它的依赖进行解耦的技术。
不是每次都在A类需要B类的实例的时候，都实例化B；
而是实例化B一次，每次A类需要的时候，自动发送给A。

Injection一般是通过constructor Injection或者setter来注入的

IOCcontainer是一个类用来管理和提供依赖的。本质上是一个工厂提供请求类型的实例的。

Repository pattern 是要在data access layer 和business logic layer之间
创建一个abstraction layer.目的是创建一个松耦合的方式访问数据库数据，也使得代码
清晰，易维护，可复用

data access logic 存储在repository里，repository也负责持久化business model。


service layer从controller里抽离逻辑，使得presentation layer和controller 整洁易维护


Onion architecture是一种layered 架构，我们目前将其分为Domain layer，Service Layer,
Infrastructure Layer 和Presentation Layer。
概念上可以将Infrastructure 和Presentation算是同一级。

所有layers彼此通过接口进行通信

Onion架构的可测试性非常高，因为所有的事物都是依赖抽象。abstraction容易被mock。


dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update


controllers should only be rseponsible for handling requests, model validation, and
returning response to the frontend or some client.

keeping business logic away from controllers is a good way to keep
them lightweight, and code more readable and maintainable.

the purpose of the presentation layer is to provide the entry point to system
so that consumers can interact with the data.

we can implement presentation layer in many ways, for example creating a
rest api, grpc, grphaql,etc.

because ASP.NET core uses Dependency Injection everywhere, we need to have a
reference to all of the projects in the solution from the main project.

asp.net core has two ways to implement routing
- convention-based routing
        need to use app.UseRouting();app.MapControllers that add endpoints for
        controller actions without specifyinh any routes.
        [contrllerName]/[actionName]/[optional parameters]
- attribute routing (recommand)
        uses the attributes to map the routes directly to the action methods inside
        the controllers.
       
the resource name in the URI should always be a noun.

the hierarchy between resources should follow the convention:
/api/principalResource/{principalId}/dependentResource
because employees can't exist without a company


a data transfer object(DTO) is an object that we use to transport data
between the client and server applications.

by using DTO, the response will stay as it was before the model changes.


extract all the exception handling logic into a single centralized place,
which makes actions cleaner, more readable, and the error handling process
more maintainable.

an application where we have to throw custom exceptions more often and
maybe impact performance.






