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
    public void Dispose()
    {
      Venue.DeleteAll();
      // Band.DeleteAll();
    }
  }
}
