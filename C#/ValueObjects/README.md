# ValueObjects

## Base
It's a base class for all value object generated

## FilterObject
I use it for map a filter parameter from POST URL. 
When the body of request is like this:
```[json]
{
    "filter": "id=1;name=bob"
}
```
The `FilterObject` help me to manage the filtering into database (most of times, with EntityFramework)