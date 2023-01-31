# Repository

A Repository is a collection-like interface that is used by the
Domain and Application Layers to access to the data
persistence system (the database) to read and write the
Business Objects, generally the Aggregates.

The primary benefit of repositories is to abstract the storage mechanism for the authoritative collection of entities.

Common Repository principles are:  
● Define a repository interface in the Domain Layer
(because it is used in the Domain and Application Layers),
implement in the Infrastructure Layer
(EntityFrameworkCore project in the startup template).  
● Do not include business logic inside the repositories.  
● Repository interface should be database provider / ORM
independent. For example, do not return a DbSet from a
repository method. DbSet is an object provided by the EF
Core.  
● Create repositories for aggregate roots, not for all
entities. Because, sub-collection entities (of an 