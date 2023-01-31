# Application Layer

The Application Service uses the Domain Objects
(Entities, Repository interfaces, Domain Services, etc.) to
implement the use case. Application Layer implements
some cross cutting concerns (Authorization, Validation,
etc.). An Application Service method should be a Unit Of
Work. That means it should be atomic.

- Use always DTOs for input and output and never return Domain objects (Entity, Aggregate..).
- Flow
    - DTO input validation?
    - construct Domain Entity passing DTO input properties as parameters
    - pass Domain Entity to repository, repository return Domain Entity ?