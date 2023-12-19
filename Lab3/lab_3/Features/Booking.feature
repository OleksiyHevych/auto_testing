Feature: Booking API Tests
    In order to test the Booking API
    As a QA engineer
    I want to interact with the API using GET, POST, PUT, and DELETE requests

Scenario: Create, Update, and Delete a Booking
    Given I have access to the bookings API
    When I send a GET request to "/booking"
    Then I receive a response with status code "OK"
    And The response contains a list of booking IDs

    Given I have valid booking details
    When I send a POST request to "/booking" with the booking details
    Then I receive a response with status code "OK"
    And The response contains the booking ID and details
   
    Given I have a booking ID from a created booking
    When I send a PUT request to "/booking/{bookingId}" with the updated details
    Then I receive a response with status code "OK"
    And The response reflects the updated booking details

    Given I have a booking ID from a created booking
    When I send a DELETE request to "/booking/{bookingId}"
    Then I receive a response with status code "Created"