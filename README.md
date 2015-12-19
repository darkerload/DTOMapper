# DTOMapper
Data transfer between two object. Assign values to the fields of the same name. 

## Install
Install via Nuget Package
        
        PM> Install-Package DTOMapper
  
## How to Use
For entity
```c#
Mapper.Bind<Source, Target>(source);
```

For list
```c#
Mapper.Bind<List<Source>, List<Target>>(listSource);
```
