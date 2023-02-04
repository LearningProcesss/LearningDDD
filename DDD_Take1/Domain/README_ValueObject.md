# Value Object

Value Objects are entity's state, describing something about the entity or the things it owns.  
Are considered enity-value object relationship that represents a particular concept.
They don't have ***Identity***.
They should comunicate explicitly important business rules and domain logics isolating\hiding primitive types that are not great to express what their concept are in the domain, furthemore, primitive types and related concepts will be scattered throughout the domain rather than being cohesively co-located, because you cannot add behaviour to primitive classes. 
They are immutable, side effect free and easy testable.

```c#
//Use a Value Object to represent a concept that has no identity

public class BankAccount 
{
    public BankAccount(Guid id, Money startingBalance)
    {

    }
}

public class Money
{


}

```