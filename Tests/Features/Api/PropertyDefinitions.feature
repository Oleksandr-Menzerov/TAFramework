Feature: Property Definitions
  As a user
  I want to be able to retrieve and interact with property definitions
  So that I can view, filter, and verify their details.

@API
Scenario: Get all property definitions
  As a user
  I want to retrieve all property definitions
  So that I can see the complete list of available properties
  When I request all property definitions
  Then I should receive a list of property definitions
  And each property definition should have required fields

@API
Scenario: Get a specific property definition
  As a user
  I want to retrieve a specific property definition by its ID
  So that I can see the details of a particular property
  Given I have a list of property definitions
  When I request the property definition with id 1
  Then I should receive the correct property definition

@API
Scenario: Validate property definition structure
  As a user
  I want to validate the structure of property definitions
  So that I can ensure all necessary fields are present and correctly typed
  Given I have a list of property definitions
  Then each property definition should have the following structure
    | Field                  | Type    |
    | Name                   | string  |
    | PropertyDefinitionType | int     |
    | IsMandatory            | boolean |
    | Id                     | int     |
    | PropertyValues         | list    |

@API
Scenario: Filter property definitions by type
  As a user
  I want to filter property definitions by their type
  So that I can focus on properties of a specific type
  Given I have a list of property definitions
  When I filter property definitions by type 0
  Then all filtered property definitions should have type 0

@API
Scenario: Verify mandatory property definitions
  As a user
  I want to filter property definitions to show only mandatory ones
  So that I can ensure all important properties are properly handled
  Given I have a list of property definitions
  When I filter mandatory property definitions
  Then all filtered property definitions should be mandatory
