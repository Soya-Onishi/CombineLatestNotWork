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
            // OutputPath��"expect"�ɂȂ�Atrue�ɂȂ�͂��ɂ��ւ�炸
            // example.IsValid.Value��false�̂܂܁B
            //
            // EN:
            // Although even if OutputPath is "expect" and example.IsValid will be true,
            // it is still false.
            Assert.True(example.IsValid.Value);

            // JP:
            // ���Assert���R�����g�A�E�g����
            // ���������s����ƁAexample.IsValid.Value��true�ɂȂ�B
            //
            // EN:
            // If running below after comment out an above Assert,
            // below example.IsValid.Value is true.
            example.Names.Add("xyz");
            Assert.True(example.IsValid.Value);
        }
    }
}