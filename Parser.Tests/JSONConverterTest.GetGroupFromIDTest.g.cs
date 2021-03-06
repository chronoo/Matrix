using System.Net;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.View;
using Parser.Model;
// <auto-generated>
// Этот файл содержит автоматически созданные тесты.
// Не изменяйте этот файл вручную.
// 
// Если содержимое файла устареет, его можно будет удалить.
// Например, если файл больше не компилируется.
// </auto-generated>
using System;

namespace Parser.View.Tests
{
    public partial class JSONConverterTest
    {

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException140()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, (string)null);
    group = this.GetGroupFromIDTest(s0, 0);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException906()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, (string)null);
    group = this.GetGroupFromIDTest(s0, 1);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException14001()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "");
    group = this.GetGroupFromIDTest(s0, 0);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException22()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, (string)null);
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException122()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, (string)null);
    group = this.GetGroupFromIDTest(s0, 464);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException70()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, (string)null);
    group = this.GetGroupFromIDTest(s0, int.MinValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException448()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\"");
    group = this.GetGroupFromIDTest(s0, 0);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException220()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, ":");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException457()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\r");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(UriFormatException))]
public void GetGroupFromIDTestThrowsUriFormatException109()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "!:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException940()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "B:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException308()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " ");
    group = this.GetGroupFromIDTest(s0, 67);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException172()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " \t");
    group = this.GetGroupFromIDTest(s0, 50);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45701()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45702()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "p\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45703()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "|\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45704()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "A\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(NotSupportedException))]
public void GetGroupFromIDTestThrowsNotSupportedException703()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "b[:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(NotSupportedException))]
public void GetGroupFromIDTestThrowsNotSupportedException70301()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "a»:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45705()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\0|:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45706()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "/\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45707()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\\\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException881()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "a0:%\0");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException88101()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "a0:%|");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException88102()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "a0:%:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException319()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " ");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45708()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\r B");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45709()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " \0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45710()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\tL:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45711()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\r\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45712()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\t\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45713()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " a\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45714()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\tp~:::");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45715()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\tL\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45716()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\t\\\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45717()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\t\0|:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45718()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\t/\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45719()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " \"\0\0:::");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45720()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\rÒS:::");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45721()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "\tZX:::");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException227()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "  ");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(WebException))]
public void GetGroupFromIDTestThrowsWebException313()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " A:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(UriFormatException))]
public void GetGroupFromIDTestThrowsUriFormatException148()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, ",:");
    group = this.GetGroupFromIDTest(s0, -4);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(UriFormatException))]
public void GetGroupFromIDTestThrowsUriFormatException583()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " ɂ:");
    group = this.GetGroupFromIDTest(s0, -46);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(NotSupportedException))]
public void GetGroupFromIDTestThrowsNotSupportedException70302()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "B@:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45722()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, " NET:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45723()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "АRÀP\0\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45724()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "GOPH\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45725()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "UUID\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45726()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "FILE\0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}

[TestMethod]
[PexGeneratedBy(typeof(JSONConverterTest))]
[PexRaisedException(typeof(ArgumentException))]
public void GetGroupFromIDTestThrowsArgumentException45727()
{
    Group group;
    JSONConverter s0 = new JSONConverter((string)null, "GOPH.0\0:");
    group = this.GetGroupFromIDTest(s0, int.MaxValue);
}
    }
}
