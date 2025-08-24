using thot.DS.Windows;

namespace thotTest;

[TestFixture]
public class DSGraphViewTest {


    [Test]
    public void ShouldCreateGraph() {
        var g = new DSGraphView();
        
        Assert.That(g, Is.Not.Null);
    }
}