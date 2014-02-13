## Valuation Engine
This project contains the building blocks for more complex valuation and decision engines like the ones used in DraftNinja and HomeGame. The objects defined describe several core concepts used in these engines.

### Universe<T>
A universe of objects is a collection of those objects and the functionality that supports complex and potentially processor-intensive valuation scenarios. Additionally, it extends IEnumerable<T> and can be used in LINQ queries.

### Comparative Valuation Engines
A CVE is a an engine that generates object valuations in relation to a universe of those objects.

### Simple Valuation Engines
SVEs are engines that generate object valuation in isolation or against an established set of metrics.

### Axe Grinders
Axe grinders are valuation modifiers that can be chained together to apply coefficients of valuation after an initial valuation algorithm.
