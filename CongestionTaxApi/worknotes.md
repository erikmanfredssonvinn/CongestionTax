### There are TODO notes and just plain comments in the code that explains what more I wanted to do and why I made some choices.

### How I prioritized the tasks and why
The priority has been to ensure that the business logic in the calculator works flawlessly and is bug-free (including the bonus task), and to write tests to verify that the code functions as intended. Concurrently, I've developed the domain classes Vehicle and CongestionTaxRule since they are used in the calculator.

Following that, my prioritization has been as follows:

1. Service classes
2. Controllers
3. Data storage

The rationale behind this prioritization has been to work from the domain classes, which contain the business logic that I consider most crucial to complete and ensure functions correctly, towards the service layer, which also contains significant logic and coordinates between different layers. Then, I prioritized getting the API up and running since it was one of the requirements for the task, and finally, the storage layer, which only contains logic for fetching data.

Lastly, I created a DI container and a simple factory for creating instances of the CongestionTaxCalculator.