using System;
using NUnit.Framework;

namespace MvcBreadCrumbs.Tests
{
    [TestFixture]
    public class BreadCrumbTest
    {
        [Test]
        public void AddDisplayDisplayRawTest()
        {
            IProvideBreadCrumbsSession pbcs = new ProvideBreadCrumbsSessionMock();

            BreadCrumb.Add("url1", "label1", 1, sessionProvider: pbcs);
            Assert.AreEqual(BreadCrumb.Display(sessionProvider: pbcs), "<ul class=\"breadcrumb \"><li><a href=\"url1\">label1</a></li></ul>");
            Assert.AreEqual(BreadCrumb.Display(addClasses: "testClass", sessionProvider: pbcs), "<ul class=\"breadcrumb testClass\"><li><a href=\"url1\">label1</a></li></ul>");
            Assert.AreEqual(BreadCrumb.DisplayRaw(sessionProvider: pbcs), "<a href=\"url1\">label1</a>");

            BreadCrumb.Add("url2", "label2", 2, sessionProvider: pbcs);
            Assert.AreEqual(BreadCrumb.Display(sessionProvider: pbcs), "<ul class=\"breadcrumb \"><li><a href=\"url1\">label1</a></li><li><a href=\"url2\">label2</a></li></ul>");
            Assert.AreEqual(BreadCrumb.Display(addClasses: "testClass", sessionProvider: pbcs), "<ul class=\"breadcrumb testClass\"><li><a href=\"url1\">label1</a></li><li><a href=\"url2\">label2</a></li></ul>");
            Assert.AreEqual(BreadCrumb.DisplayRaw(sessionProvider: pbcs), "<a href=\"url1\">label1</a> > <a href=\"url2\">label2</a>");
        }

        class ProvideBreadCrumbsSessionMock : IProvideBreadCrumbsSession
        {
            public string SessionId
            {
                get
                { return "01"; }
            }
        }
    }
}
