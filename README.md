# RuntimeDiagnosis
Runtime diagnoses to read and manipulate members in .NET based projects.

This diagnosis is made possible by generating either derived classes (where necessary from the existing architecture) or decorators (more advisable, as they can be easily integrated via dependency injection containers). The members are encapsulated in these and supplemented with logic for the diagnosis.

Note 1: In the following, getters and setters are used not only for properties but also for fields. This is because fields are encapsulated in properties to enable diagnosis.
Note 2: The following refers to input and output data in relation to members. What is meant by this is:
- for fields and properties: input = setter; output = getter
- for methods: input = parameter; output = return value
- for events: input = parameter when invoking; output: parameters that are passed to event handlers.
  - these are of course normally identical, but a distinction is made between them here because they can be manipulated independently of each other (more on this below).

Enables direct investigation when debugging is not possible by:
- viewing the uses of members
- counting how often the value of a member was retrieved and how often it was changed.
- showing by whom a member was last called (for fields and properties subdivided for input (setter) and output (getter))
- reading current values of fields and properties
- which parameters were last passed in methods
- can see which return values methods and events (as parameters for the event handler) have last transmitted
- manipulate the input and output data of members:
  - in the case of input data, the member is invoked with this data (invocation of setters for fields and properties, methods and events are invoked with the specified data as parameters)
  - for output data, the member is NOT called, but instead the manipulated data is returned when it is called (via the getter of fields and properties, as return value for methods and as parameters to the event handler for events)
- recalling the members with the last input data and recalling the getter of fields and properties to retrieve the current value and recalling the setter to set the same value again.
- optional logging of status changes and evaluation of these logs to show a history.
