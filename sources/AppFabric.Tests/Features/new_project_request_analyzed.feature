Feature:NewProjectRequest
  To have some work done
  As a regular client 
  I want to create a project request

  Scenario Outline: Analisar uma requisição de Projeto 
    Given The client <clientId>, requested a project named <name>, code <code>, budget <budget>, and start date <date>
    When The client request a project
    Then The client see a project request created equals <created>
  
    Examples:
    |clientId|name|code|budget|date|created|
    |A3D46E71-B22F-4A7F-B7F7-4983FB6EDDA4|My First Project | MyProject | 13| 12/31/2020 | true|
    |29AB8142-C5D3-4368-90B3-819748093AAE|My First Project | MyProject | 13.2| 12/31/2020 | true|
    |BA6F9193-A51A-4950-8A49-690875FCEF39|My First Project 1 | MyProject1 | 13.4| 1/1/1900 | false|