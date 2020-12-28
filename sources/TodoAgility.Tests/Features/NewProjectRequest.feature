Feature:NewProjectRequest
  To have some work done
  As a regular client 
  I want to create a project request 
  
  Scenario Outline: Create a Project Request
    Given Name <name>, code <code> and start date <date>
    When The client request a project
    Then The client see a project request created equals <created>
  
    Examples:
    |name|code|date|created|
    |My First Project | MyProject | 12/31/2020 | true|