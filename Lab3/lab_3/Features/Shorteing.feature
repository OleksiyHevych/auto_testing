Feature: URL shortening service
  In order to share links efficiently
  As a user of the cleanuri API
  I want to shorten long URLs and ensure they redirect correctly

  Scenario: Shortening a valid URL
    Given I have a valid long URL "http://google.com/"
    When I request to shorten the URL
    Then I should receive a shortened URL
    And the shortened URL should redirect to the original URL

  Scenario: Attempting to shorten a URL with spaces
    Given I have a valid long URL "http://example.com/this is a test"
    When I request to shorten the URL
    Then I should receive an error message