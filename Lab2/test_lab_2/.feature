Feature: Bank Manager Login functionality

Scenario: Sorting customers by Last Name
    Given I am on the Bank Manager login page
    When I login as a Bank Manager
    Then I should see the manager dashboard
    When I click on Customers menu item
    Then I should see the list of customers
    When I click on Last Name column header
    Then the customers should be sorted by their last names
