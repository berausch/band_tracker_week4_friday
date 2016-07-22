using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace bandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
        int result = Band.GetAll().Count;
        Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Band firstBand = new Band("Queen");
      Band secondBand = new Band ("Queen");

      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Test_Save_SavesBandToDatabase()
    {
      Band testBand = new Band("Queen");
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testBand = new List<Band>{"Queen"};
      Assert.Equal(testBand, result);
    }
    [Fact]
    public void Test_Save_AssignsIdtoBandObject()
    {
      Band testBand = new Band("Queen");
      newBand.Save();
      Band savedBand = Band.GetAll()[0];
      int result = testBand.GetId();
      int testId = savedBand.GetId();

      Assert.Equal(result, testId);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
