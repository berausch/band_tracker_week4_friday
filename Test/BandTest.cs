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
      List<Band> savedBand = new List<Band>{testBand};
      Assert.Equal(savedBand, result);
    }
    [Fact]
    public void Test_Save_AssignsIdtoBandObject()
    {
      Band testBand = new Band("Queen");
      testBand.Save();
      Band savedBand = Band.GetAll()[0];
      int result = testBand.GetId();
      int testId = savedBand.GetId();

      Assert.Equal(result, testId);
    }
    [Fact]
    public void Test_Find_FindBandInDatabase()
    {
      Band testBand = new Band("Queen");
      testBand.Save();
      Band foundBand = Band.Find(testBand.GetId());
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void Test_GetVenues_GetsAllVenuesThatThisBandPlayed()
    {
      Venue test1Venue = new Venue("Roxy");
      test1Venue.Save();

      Venue test2Venue = new Venue("Roxy");
      test2Venue.Save();

      Band testBand = new Band("The Police");
      testBand.Save();
      testBand.AddVenue(test1Venue);

      List<Venue> result = testBand.GetVenues();

      List<Venue> expectedResult = new List<Venue> {test1Venue};

      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
      Venue test1Venue = new Venue("Roxy");
      test1Venue.Save();

      Venue test2Venue = new Venue("Roxy");
      test2Venue.Save();

      Band testBand = new Band("The Police");
      testBand.Save();

      testBand.AddVenue(test1Venue);
      testBand.AddVenue(test2Venue);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{test1Venue, test2Venue};
      Assert.Equal(testList, result);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
