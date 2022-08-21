using CombineLatestNotWork;

namespace ExampleUnitTest
{
    public class ExampleTest
    {
        [Fact]
        public void Test()
        {
            var example = new ExampleClass("actual", "expect", new[] { "abc", "def", "ghi" });

            Assert.False(example.IsValid.Value);

            example.OutputPath.Value = "expect";

            // JP:
            // OutputPathが"expect"になり、trueになるはずにも関わらず
            // example.IsValid.Valueがfalseのまま。
            //
            // EN:
            // Although even if OutputPath is "expect" and example.IsValid will be true,
            // it is still false.
            Assert.True(example.IsValid.Value);

            // JP:
            // 上のAssertをコメントアウトして
            // ここを実行すると、example.IsValid.Valueはtrueになる。
            //
            // EN:
            // If running below after comment out an above Assert,
            // below example.IsValid.Value is true.
            example.Names.Add("xyz");
            Assert.True(example.IsValid.Value);
        }
    }
}