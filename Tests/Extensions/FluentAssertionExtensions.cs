using FluentAssertions.Collections;
using System.Reflection;

namespace Tests.Extensions
{
    /// <summary>
    /// Provides extension methods for FluentAssertions to allow custom assertions on collections.
    /// </summary>
    public static class FluentAssertionExtensions
    {
        /// <summary>
        /// Asserts that a collection is ordered by a specified property in the given direction (ascending or descending).
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="assertions">The collection assertions to extend.</param>
        /// <param name="propertyName">The name of the property to order by.</param>
        /// <param name="direction">The direction to sort the collection ("ascending" or "descending").</param>
        /// <param name="because">A formatted phrase explaining why the assertion should be satisfied. If the phrase does not start with the word "because", it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{T}"/> to allow for method chaining in FluentAssertions.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="assertions"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the specified property does not exist on type <typeparamref name="T"/> or when an invalid sort direction is provided.</exception>
        public static AndConstraint<GenericCollectionAssertions<T>> BeOrderedBy<T>(
                this GenericCollectionAssertions<T> assertions,
                string propertyName,
                string direction,
                string because = "",
                params object[] becauseArgs)
        {
            ArgumentNullException.ThrowIfNull(assertions);

            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ?? throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'.");
            object propertySelector(T item) => propertyInfo.GetValue(item)!;

            IOrderedEnumerable<T> orderedEnumerable;
            if (direction.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                orderedEnumerable = assertions.Subject.OrderBy(propertySelector);
            }
            else if (direction.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                orderedEnumerable = assertions.Subject.OrderByDescending(propertySelector);
            }
            else
            {
                throw new ArgumentException("Invalid sort direction");
            }

            // Assert that the collection is equivalent to the ordered version, with strict ordering enforced.
            assertions.Subject.Should().BeEquivalentTo(orderedEnumerable, options => options.WithStrictOrdering(), because, becauseArgs);

            return new AndConstraint<GenericCollectionAssertions<T>>(assertions);
        }
    }
}
