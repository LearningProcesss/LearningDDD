# Value Object

Value Objects are entity's state, describing something about the entity or the things it owns.  
Are considered enitty-value object relationship that represents a particular concept.
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

Choose to use a Value Object with these characteristics:

1. measures, quantifies or describes a thing in the domain.
2. can be mantained as immutable.
3. models a conceptual ***whole*** by composing related attributes as an integral unit.
4. is ompletely replaceable when the measurement or description changes.
5. can be compared with others using Value equality.
6. supplies its collaborators with Side-Effect-Free Behaviour.  


Details

1. measures, quantifies or describes a thing in the domain
A Person Value Object has Name field, that is not really a thing in the domain, but but measures or quantifies the number of years the person (thing)
has lived.

2. immutable  
After initialization with constructor, no properties nor method to alter Value state should be exposed.
When Value must change a complete replacement of the entire object should be done instead.

3. conceptual whole  

