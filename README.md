# 2DGameToolkit

A 2D game toolkit for game jam purposes.

## Content


## Contributing 

### Naming

| Item    | Prefix  |   Case     |
|---------|---------|------------|
| class   |         | PascalCase |
| method  |         | PascalCase |
| Interface  |   I      | PascalCase |
| Interface  |   E      | PascalCase |
| static member  |   ms_      | PascalCase |
| other member  |   m_      | PascalCase |
|---------------|-----------|------------|

### Brackets

Always place on a newline

### if statement

Always use brackets even if their is only one line:

if(condition)
{
    a = 1;
}

### Comments

Always use // for one line and /** **/ for multiline.

### UnitTest

Unit testing is done with the **MSTest** framework.
You will find unit test in the **2DGameToolKit/** folder.
Only Unity agnostic functionalities is tested (i.e. scripts in the **Engine/** folder).
Please always make torough test when you add new features, as it provides both stability and a good example of how to use your code.

### Documentation

Documentation is done with **Doxygen**.
Please always document your code using the /** **/ comment style.
Do not document self explanatory method such as getter / setter.
