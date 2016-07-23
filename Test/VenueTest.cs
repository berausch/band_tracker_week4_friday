using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace bandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Venue firstVenue = new Venue("Roxy");
      Venue secondVenue = new Venue("Roxy");

      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();

      List<Venue> allVenues = Venue.GetAll();
      List<Venue> result = new List<Venue>{testVenue};

      Assert.Equal(result, allVenues);

    }
    [Fact]
    public void Test_Save_AssignsIdtoVenueObject()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testResult = testVenue.GetId();

      Assert.Equal(testResult, result);

    }
    [Fact]
    public void Test_Find_FindVenueInDatabase()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();
      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Test_GetBands_GetsAllBandsThatPlayedAtThisVenue()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();

      Band test1Band = new Band("Queen");
      test1Band.Save();

      Band test2Band = new Band("The Police");
      test2Band.Save();
      testVenue.AddBand(test1Band.GetId());

      List<Band> result = testVenue.GetBands();

      List<Band> expectedResult = new List<Band> {test1Band};

      Assert.Equal(expectedResult, result);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
