/**************************************************************************
 * Script:        Variables
 * Author:        DebugPadawan (https://github.com/DebugPadawan)
 * Created on:    19.01.2025
 * Description:   A demonstration of variables, their types, and usage in C#.
 **************************************************************************/

class Variables
{

    // ───────────────────────────────────────────────
    // What is a variable?
    // ───────────────────────────────────────────────
    // A variable is a named storage location in memory
    // that holds a value. It has a specific data type
    // which defines the kind of data it can store.

    // Basic Syntax: 
    // <data_type> <variable_name> = <value>;

    // ───────────────────────────────────────────────
    // Data Types:
    // ───────────────────────────────────────────────

    // Value Types:
    //    - int:    32-bit signed integer                  
    //    - float:  32-bit single-precision floating point number   
    //    - double: 64-bit double-precision floating point number  
    //    - char:   16-bit Unicode character       
    //    - bool:   Boolean value (true or false)
    //    - byte:   8-bit unsigned integer
    //    - short:  16-bit signed integer
    //    - long:   64-bit signed integer
    //    - decimal: 128-bit decimal value
    //    - string: A sequence of characterc

    // ───────────────────────────────────────────────
    // Initializing Variables:
    // ───────────────────────────────────────────────

    // Variables can be initialized when they are declared.
    // If a variable is not initialized, it will have a default value.

    // Default Values:
    //    - int:    0
    //    - float:  0.0f
    //    - double: 0.0d
    //    - char:   '\0'
    //    - bool:   false
    //    - byte:   0
    //    - short:  0
    //    - long:   0
    //    - decimal: 0.0m
    //    - string: null

    // ───────────────────────────────────────────────
    // Example:
    // ───────────────────────────────────────────────

    int score = 100;
    float health = 100.0f;
    double distance = 100.0d;
    char grade = 'A';
    bool isAlive = true;
    byte age = 25;
    short weight = 70;              
    long population = 8000000000;  
    decimal money = 100.0m;         
    string name = "John Doe";

    // ───────────────────────────────────────────────
    // Coding Conventions:
    // ───────────────────────────────────────────────

    // - Use meaningful names for variables.            (e.g. score, health, distance)
    // - Use camelCase for variable names.              (e.g. myVariable)
    // - Use PascalCase for class names.                (e.g. MyClass)
    // - Use underscores for private fields.            (e.g. _myField)
    // - Use all uppercase for constants.               (e.g. MAX_HEALTH)
    // - Use descriptive names for constants.           (e.g. MAX_HEALTH instead of MAX_HP)
    // - Use the var keyword for local variables.       (e.g. var myVariable = 100;)
    // - Use the explicit type for public variables.    (e.g. int myVariable = 100;)
    // - Use the explicit type for method parameters.   (e.g. void MyMethod(int myParameter) { ... })
    // - Use the explicit type for return values.       (e.g. int MyMethod() { return 100; })


}