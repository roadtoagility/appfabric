Feature:NewProjectRequest
  To have some work done
  As a regular client 
  I want to create a project request

  Scenario: Client Todo a Project Request
    Given Name My Project, code MyProject and start date 12/31/2020
    When The client request a project
    Then The client see a project request created equals true
    
  Scenario Outline: Creating a Project Requests
    Given Name <name>, code <code> and start date <date>
    When The client request a project
    Then The client see a project request created equals <created>
  
    Examples:
    |name|code|date|created|
    |My First Project | MyProject | 12/31/2020 | true|
    |My First Project 1 | MyProject1 | 1/1/1900 | false|