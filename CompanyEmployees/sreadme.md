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





