global using FluentAssertions;
global using TechTalk.SpecFlow;
global using NUnit.Framework;
// Specifies that the test fixtures can run in parallel
[assembly: Parallelizable(ParallelScope.Fixtures)]
// Sets the maximum degree of parallelism to 4, allowing up to 4 test fixtures to run concurrently
[assembly: LevelOfParallelism(4)]
