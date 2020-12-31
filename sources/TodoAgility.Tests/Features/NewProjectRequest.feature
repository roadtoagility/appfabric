Feature:NewProjectRequest
  To have some work done
  As a regular client 
  I want to create a project request

  Scenario: Client Todo a Project Request
    Given The client 10, requested a project named My Project, code MyProject, budget 5, and start date 12/31/2020
    When The client request a project
    Then The client see a project request created equals true
    
  Scenario Outline: Creating a Project Requests
    Given The client <clientId>, requested a project named <name>, code <code>, budget <budget>, and start date <date>
    When The client request a project
    Then The client see a project request created equals <created>
  
    Examples:
    |clientId|name|code|budget|date|created|
    |10|My First Project | MyProject | 13| 12/31/2020 | true|
    |10|My First Project | MyProject | 13.2| 12/31/2020 | true|
    |1|My First Project 1 | MyProject1 | 13.4| 1/1/1900 | false|