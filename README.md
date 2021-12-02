Добавить в класс `EnumerableSequences` перегруженные версии обобщенно-типизированных методов расширений типизированного интерфейса [`IEnumerable<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-6.0), используя в качестве передаваемых параметров (стратегий фильтрации, трансформации, сравнения) соответствующие делегаты
 - [`Predicate<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-6.0)
 - [`Converter<TInput,TOutput>`](https://docs.microsoft.com/en-us/dotnet/api/system.converter-2?view=net-6.0)
 - [`Comparison<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.comparison-1?view=net-6.0)

При реализации перегруженных версий методов использовать: 
 - для методов (`Filter`, `SortBy`) с параметрами делегатами вызовы методов с параметром интерфейсом;
 - для метода (`Transform`) с параметром интерфейсом вызов метода с параметром делегатом.

Добавить новые тесты для проверки методов с параметрами делегатами.
