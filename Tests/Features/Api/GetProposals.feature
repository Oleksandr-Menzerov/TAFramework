Feature: Get Proposals
    As a user,
    I want to retrieve a list of proposals
    so that I can view and manage the available proposals based on specific conditions like top results, skipped results, sorting, and filtering.

Background:
  Given I have retrieved all existing proposals

@API
Scenario Outline: Get top N proposals
    As a user,
    I want to retrieve the top <topN> proposals
    so that I can see only the first <topN> proposals from the available list.
  When I request proposals with top <topN>
  Then I should see <expectedCount> proposals in the result

  Examples:
    | topN | expectedCount |
    | 4    | 4             |
    | 2    | 2             |
    | 3    | 3             |

@API
Scenario Outline: Skip N proposals
    As a user,
    I want to skip the first <skipN> proposals
    so that I can view the list starting from the <nthProposal> proposal.
  When I request proposals with skip <skipN>
  Then the first proposal should match the <nthProposal> proposal from all proposals

  Examples:
    | skipN | nthProposal |
    | 3     | 4           |
    | 1     | 2           |
    | 2     | 3           |

@API
Scenario Outline: Order proposals
    As a user,
    I want to order proposals by a specific <field> in <direction> order
    so that I can see the proposals sorted based on my selected criteria.
  When I request proposals ordered by <field> <direction>
  Then the proposals should be sorted by <field> in <direction> order

  Examples:
    | field     | direction  |
    | Price     | ascending  |
    | Price     | descending |
    | CreatedOn | ascending  |
    | CreatedOn | descending |

@API
Scenario Outline: Filter proposals
    As a user,
    I want to filter proposals by specific conditions
    so that I can see only the proposals that satisfy the filter "<filterCondition>".
  When I filter proposals with "<filterCondition>"
  Then all returned proposals should satisfy the condition "<filterCondition>"

  Examples:
    | filterCondition         |
    | Price gt 100            |
    | Price lt 100            |
    | CreatedOn gt 2024-01-01 |
    | IsActive eq true        |
