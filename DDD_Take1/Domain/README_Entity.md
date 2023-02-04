## Entity

As developers we tend to focus on data rather than the domain,
instead of designing domain concepts with rich behaviours we might think primarly about database side like attribues (columns) and associations (FK).  
This approch leads to so called ***Entity*** abounding of public get and set that do not describes any behaviour.  

***Entity*** is an unique thing and is capable of being changed continuosly but is always recognizable by its own ***Identity***.  
prior to that differs from ***Value Object***.  

***Entity*** is composed by ***Unique Identity***, attributes expressed by primitives, collections and by behaviours expressed with by methods or delegated to ***Value Object***.  

In a CRUD system everithing is a value but a system like that can't stands   
when comlexity grows, too complicated business rules comes in, and where DDD and Entity shines with a low control level.  

### Unique Identity

> In the early stage of designing ***Entity*** focus only on primary attributes\characteristics particurarly on those that identify it and add behaviour that is essential to the concept and attributes that are requireds by that behaviour.

Can be a primitive like Guid, string, int, long or ***Value Object*** that serves as holders of the Entiti's unique identity with specific and centralized behaviour.  

Identity stability is mandatory for the entire Entiti's lifecycle.  
Private setters or Guard must be used to hide or prevent field used as or part of the Identity.

Two Entities are considered equal if they have same Identity even when they have rest of the state completly different or just one property.

***Common Identity creation strategies***

1.  *User Provides Identity*  
    User provides data used to generate Identity, this approach is initially cheap, but,    
    are those good quality data? like a "Title" field can be mispelled or simply incorrect and user wants to change it in the future. Identity are readonly and immutable.
2.  *Application Generates Identity*  
    GUID is a fast standard approach that can be autogenerated or by a given text.
    In a time critical business lot of GUID can be preallocated to a cache.   
    Custom UUID\string (not compatible GUID) can be composed to be more readable not ony for clients but for developers and can contains specific words of the ***Bounded Context*** where it was generated like APM-P-08-14-2012-F36AB21C  (APM = Boundend Context) (P = Product Entity type) (08-12-2012 = creation date) (F36AB21C = first segment of autogenerated GUID)  
    Identity creation, that is a detail, is not matter of ***Domain*** but ***Infrastructure***. Can be ***Repository*** (for ***Aggregate***) or ***Factory*** or External Service as well.  
    A ***Value Object*** is far more suitable to hold this information rather than in a string.  (my assumption: Value Object is used just to hold Identity and expose only interpretation and not creational behaviour)  
    Using a ***Value Object*** as Identity holder prevent a so called "shotgun surgery" if you decide to change identity type of your ***Entity*** or ***Aggregate***

    ```c#
    // Vaughn Vernon
    public class ProductRepository : IProductRepository
    {
        public Guid NextIdAutoGenerated() 
        {
            return Guid.NewGuid();
        }

        // my assumption
        public string NextIdComposed()
        {
            string boundedContext = "..";
            string type = "..";
            string date = "..";
            string guid = Guid.NewGuid().ToString().Substring(..);
            return $"{boundedContext}-{type}-{date}-{guid}";
        }
    }

    public class ProductId : ValueObject
    {
        private readonly string id;

        public ProductId(string id)
        {
            this.id = id;
        }

        public string GeneratedFromBoundedContext()
        {
            return this.id..;
        }
    }

    public class Product : Entity
    {
        private ProductId productId;

        public Product(ProductId productId..)
        {
            ..
        }

        public string GeneratedFromBoundedContext()
        {
            return productId.GeneratedFromBoundedContext();
        }
    } 
    ```

    ```c#
    // Scott Millet e Nick Tunes
    // pag. 363 - 369
    // They dont use Repository but Factory for static incremental number generation.
    // Custom UUID\string creation is matter of the Entity itself:

    public class HolidayBooking 
    {
        public string Id { get; private set; }

        public HolidayBooking(int travelerId, DateTime fistNight, DateTime lastNight, DateTime booked)
        {
            ..
            this.Id = GenerateId(travelerId, firstNight, lastNight, booked);
        }

        private string GenerateId(int travelerId, DateTime fistNight, DateTime lastNight, DateTime booked)
        {
            return string.Format("{0}-{1}-{2}-{3}", travelerId, fistNight, lastNight, booked);
        }
    }
    ```
    Questions:  
    If Repository generates Id only for Aggregate, who generate Entity Ids contained in the Aggregate boundary?
3.  *Persistence Mechanism Generates Identity*  
    Persitence mechanism is always sure to be unique. The trade off is performance.  
    A cache mechanism to store a number of preallocated identities is viable if the trade off of loosing them at restart is not a problem.  
    Sometimes it matters when the identity generation and assignment occur for an Entity and only if the Entntiy can lives without identity for a while.  
    *Early identity generation*: before Entity is persisted.
    *Late identity generation*: when the Entity is peristed.
    One possibile solution for *Early identity generation* is to create a SQL Table with a single integer\long column that will store "nextval" and the repository get this value and immediatly after update the table with nextval + 1.
4.  *Another Bounded Context Assigns Identity*   
    TODO

***When Timing of Identity Generation Matters***


***Surrogate Identity***


### Design Entity

Using ***Ubiquitos Language*** and speaking with domain experts design an entity stripping down with requirements

***Characteristics***

- recognised by requirements, is the object uniquely identified? should the object support changes over time?  
  If both yes the object is clearly an ***Entity***
- what are those properties that never can be changed and define the entity identity?
- which type of ***Identity***? simple string\Guid\long or ***Value Object***?
- what are the most basic propertyies immediately implementable?
- skip Aggregate at first, but take note for future.

***Behaviours***

- Should be behaviour oriented. Entity should expose expressive methods that communicate domain beahviour instead of exposing state.  
- encapsulate behaviour in Entity method but avoid placing too much responsability to it.  
  Delegate to a ***Value Object***, part of the Entity, logical group of immutable data and behaviours.  
  Ideally, you should always put most of the business logic into value objects. Entities in this situation would act as wrappers upon them and represent more high-level functionality.  
  Recognize Value Object when data are immutable but belongs to the Entity.  
  Depth object graph accepted is 3 like Entity.ValueObject.ValueObjectBehaviour
- Try to model all state altering operations as verbs that correspond to business operations. A setter will only tell you what property you are changing but not why.
- if you dont find a perfect fit for a behaviour in any Entity maybe higher level coordinator is needed, place this beahaviour in ***Domain Service***

### Construction

When instantiate and Entity use a constructor that captures enough state to fully identify it and enable clients to find it.  
If you use early identity generation it must be passed to constructor.
Each constructor parameter must be delegated to the respective setter that self-encapsulate contractual condition (validation).  
Use a ***Factory*** for complex Entity instantiation.  

### Validation

Vaughn Vernon expose three major validation concepts: **Validating Attributes/Properties**, **Validating Whole Object**, **Validating Composition of objects**.  
Those applies to fundamental DDD validation concepts: **always valid - always contextually valid**, **invariant**, **deferred validation**.

One of the main aspect and golden rule advocate that an ***Entity*** must always have consistent state (always valid) but "always contextually valid" also.  
"always valid" can be achieved with **self-encapsulation** validation.     
"always contextually valid" when your validation have to be aligned with business requirements.  

**Invariants** are generally business rules/enforcements/requirements that you impose to maintain the integrity of an object at any given time.  
Scott Millet and Nick Tunes, says:
"Given that Entity or Value Object (an Hotel for example), what makes this type really this type? What does it mean for a type to be that type?"

You can think about **validation** as the process of approving a given object state,  
while **invariant** enforcement happens before that state has even been reached.  

This lead to split two concept:

- Validation Rules are re-usable parts of logic that perform validation on an entity, of which that validation can range from simple data integrity to a state validation, and whose primarily goal is to validate the entity before an action is taken on that entity.  

- Business Rules are re-usable parts of logic that perform actions on an entity, based on certain conditions that are evaluated against the entity.  Business Rules do not perform any validations, rather they perform actions based on conditions that is defined by the rule itself.

***Validating Attributes/Properties - always valid/always contextually valid/invariant*** 

Use property self-encapsulation to prevent any type of invalid state on both ***Entity*** and ***Value Object***.

> self-encapsulation  
Design your classes so that all access to data, even fron within the same class, goes through accessor methods

```c#
/*
Value Object EmailAddress is "always valid" thanks to the self-encapsulation done at initialization time, it's state is guarantee that is always consistent and valid.
*/
public class EmailAddress 
{
    private string emailAddress { get; private set; }

    public EmailAddress(string emailAddress)
    {
        SetEmailAddress(emailAddress);
    }

    private void SetEmailAddress(string emailAddress)
    {
        DomainExcpetionOfYourType.ThrowIfNullOrEmpty(emailAddress, nameof(emailAddress));

        if(emailAddress.lenght > 100)
        {
            throw new DomainExcpetionOfYourType();
        }

        if(!emailAddress.regex..)
        {
            throw new DomainExcpetionOfYourType();
        }

        this.emailAddress = emailAddress;
    }
}

/*
Entity FlighBooking is "always contextually valid" when it's behaviour Reschedule is invoked.
It ensure through business policy that a fligh cant be changed if confirmed. 
*/
public class FlighBooking
{
    public DateTime Departure { get; private set; }
    
    private bool confirmed = false;

    public FlighBooking(Guid id, DateTime departureDate)
    {
        // self-encapsulation
    }

    public void Reschedule(DateTime newDeparture)
    {
        if(confirmed) { throw new DomainExcpetionOfYourType(); }

        Departure = newDeparture;
    }

    public void Confirm()
    {
        confirmed = true;
    }
}

/*
Enity Hotel invariant rule is applied before state can reach properties.
*/
public class Hotel
{
    public Guid Id { get; private set; }
    public HotelRoomSummary Rooms { get; private set; }

    public Hotel(Guid id, HotelRoomSummary rooms, ..)
    {
        EnforceInvariant(rooms);
        this.Id = id;
        this.Rooms = rooms;
        ..
    }

    private void EnforceInvariant(HotelRoomSummary rooms)
    {
        bool hotelIsNotAnHotel = rooms.NumberSingleRooms < 1
                                 &&
                                 rooms.NumberDoubleRooms < 1
                                 &&
                                 rooms.NumberFamilyRooms < 1;

        if(hotelIsNotAnHotel)
        {
            throw new DomainExcpetionOfYourType(); 
        }
    }
}

```

**Other approaches**

***Specification Pattern***

Business Rules are might be encapsulated into dedicated class in a form of Specification, so the class itself by it's name and it's method composition, defined the rule itself.  
Specification usuallymust have access to the entire Entity state receivng the whole Entity as parameter.  


```c#

// Scott Millet

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}

public class NoDepartureReschedulingAfterBookingConfgirmation : ISpecification<FlightBooking>
{
    public bool IsSatisfiedBy(FlightBooking booking)
    {
        return !booking.Confirmed;
    }
}

public class FlightBooking
{
    private NoDepartureReschedulingAfterBookingConfgirmation noDepartureReschedulingSpec = new NoDepartureReschedulingAfterBookingConfgirmation();

    private DateTime DepartureDate {get; private set; }

    public void Reschedule(DateTime newDeparture)
    {
        if(!spec.IsSatisfiedBy(this)) { throw new DomainExcpetionOfYourType(); }

        this.DepartureDate = newDeparture;
    }
}

// Specification can be composed

```

***Is Valid***

The benefit of this approach is that it allows you to concentrate the relevant domain logic within the aggregate thus preventing its leakage. The knowledge regarding what it means to be ready for delivery should clearly belong to the Order class itself as that knowledge fully depends on the data that resides inside that class.

There’s a drawback to this implementation too, and it’s quite substantial. In order to validate itself, the entity must enter an invalid state first. And that means the aggregate no longer maintains its consistency boundary. Its invariants are held loosely and can be broken at any time.

This drawback is a deal-breaker. Invariants that lie within an aggregate should be kept by that aggregate at all times. Their violation leads to many nasty situations, potential data corruption is one of them.

```c#

public class Order
{
    public OrderStatus Status { get; private set; } = OrderStatus.InProgress;
    public string DeliveryAddress { get; set; }
    public DateTime DeliveryTime { get; set; }
 
    public IReadOnlyList<string> ValidateForDelivery()
    {
        var errors = new List<string>();
 
        if (string.IsNullOrWhiteSpace(DeliveryAddress))
            errors.Add("Must specify delivery address");
 
        if (DeliveryTime.DayOfWeek == DayOfWeek.Sunday)
            errors.Add("Cannot deliver on Sundays");
 
        return errors;
    }
 
    public void Deliver()
    {
        Status = OrderStatus.Delivering;
    }
}

// inside Controller - Application UseCase - DomainService

Order order = GetFromDatabase(model.OrderId);

order.DeliveryAddress = model.Address;

order.DeliveryTime = model.Time;

IReadOnlyList<string> errors = order.ValidateForDelivery();

if (errors.Any())
{
    // ModelState.AddModelError("", string.Join(", ", errors));

    // return View();
}

order.Deliver();

```

***TryExecute pattern***

Make the Deliver method do all the validations needed and either proceed with the delivery or return back any errors it encounters.  

From the domain model purity standpoint, this approach is much better. The entity here both holds the domain knowledge and maintains its consistency. It’s impossible to transition it into an invalid state, the invariants are guaranteed to be preserved.

```c#
public class Order
{
    public OrderStatus Status { get; private set; } = OrderStatus.InProgress;
    public string DeliveryAddress { get; private set; }
    public DateTime DeliveryTime { get; private set; }
 
    public IReadOnlyList<string> Deliver(string address, DateTime time)
    {
        var errors = new List<string>();
 
        if (string.IsNullOrWhiteSpace(address))
            errors.Add("Must specify delivery address");
 
        if (time.DayOfWeek == DayOfWeek.Sunday)
            errors.Add("Cannot deliver on Sundays");
 
        if (errors.Any())
            return errors;
 
        DeliveryAddress = address;
        DeliveryTime = time;
        Status = OrderStatus.Delivering;
 
        return errors;
    }
}
 
public class OrderController
{
    public ActionResult Deliver(DeliveryViewModel model)
    {
        Order order = GetFromDatabase(model.OrderId);
 
        IReadOnlyList<string> errors = order.Deliver(model.Address, model.Time);
        if (errors.Any())
        {
            ModelState.AddModelError("", string.Join(", ", errors));
            return View();
        }
 
        // Save the order and redirect to a success page
    }
}
```

***Validating Whole Object\Validating Composition of objects - deferred***

Vaughn Vernon use a sort of Specification + Observer Pattern as notification.

Scott Millet use 

From my understanding any deferred business rule validation has the major downside of put an Entity in a possible invalid first
and only after the completion of Entity behaviour a client might check the business rule validation.
Plus seems that will be trigger from the client using a Entity IsValid\Valid method.

## Memento Pattern

TODO

## Resources used for this topic

|link|reasons|
|---|--|
|https://vaadin.com/blog/ddd-part-2-tactical-domain-driven-design|Entity behaviors|
|https://enterprisecraftsmanship.com/posts/entity-vs-value-object-the-ultimate-list-of-differences/|Where place behaviours|
|https://stackoverflow.com/questions/30190302/what-is-the-difference-between-invariants-and-validation-rules|Validation and invariants|
|https://www.youtube.com/watch?app=desktop&v=JZetlRXdYeI|Always valid|
|https://enterprisecraftsmanship.com/posts/validation-and-ddd/|Domain Validations|
|https://enterprisecraftsmanship.com/posts/always-valid-vs-not-always-valid-domain-model/|Always valid|
|http://www.kamilgrzybek.com/design/domain-model-validation/|Domain Validations|
|https://enterprisecraftsmanship.com/posts/specification-pattern-always-valid-domain-model/|Domain Validation|
|https://web.archive.org/web/20130117134221/http://codeinsanity.com/archive/2008/12/02/a-framework-for-validation-and-business-rules.aspx|Validation vs Business Rules and Specification|