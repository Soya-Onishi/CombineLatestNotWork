using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace CombineLatestNotWork
{
    public class ExampleClass
    {
        public ReactiveCollection<string> Names { get; }
        public ReactiveProperty<string> OutputPath { get; }
        public ReadOnlyReactivePropertySlim<bool> IsValid { get; }
        string ExpectValue { get; }

        public ExampleClass(string actual, string expect, IEnumerable<string> names)
        {
            Names = new ReactiveCollection<string>();
            foreach (var name in names)
            {
                Names.AddOnScheduler(name);
            }

            OutputPath = new ReactiveProperty<string>(actual);
            ExpectValue = expect;

            // JP:
            // Namesがすべて別の値かどうか見る。別の値ならtrue。
            //
            // EN:
            // Check whether Names are different values. If all values are different, true.
            var namesNotConflict = Names.CollectionChangedAsObservable().Select(_ => Names.Distinct().Count() == Names.Count());

            // JP:
            // OutputPathの値がExpectValueと同じか見る。同じならtrue。
            //
            // EN:
            // Check OutputPath's value is same as ExpectValue. If same, true.
            var isExpectValue = OutputPath.Select(x => x == ExpectValue);

            var initValue = names.Distinct().Count() == names.Count() && actual == expect;

            // JP:
            // namesNotConflictとisExpectValueがどちらもtrueならIsValidもtrueになる。
            // どちらかもしくは両方がfalseならIsValidはfalseになる。
            // 
            // EN:
            // If namesNotConflict and isExpectValue are true, IsValid is also true.
            // If namesNotConflict and/or isExpectValue are false, IsValid is false.
            IsValid = new[] { namesNotConflict, isExpectValue }.CombineLatest().Select(x => x.All(y => y)).ToReadOnlyReactivePropertySlim(initValue);
        }
    }
}