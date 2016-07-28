using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace bandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Delete["/venue/{id}/delete"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", SelectedVenue);
        model.Add("bands", VenueBands);
        model.Add("venuesall", allVenues);
        model.Add("bandsall", allBands);
        return View["venueBands.cshtml", model];
      };

      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedBand = Band.Find(parameters.id);
        List<Venue> VenueBands = SelectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("bands", SelectedBand);
        model.Add("venues", VenueBands);
        model.Add("venuesall", allVenues);
        model.Add("bandsall", allBands);
        return View["bandVenues.cshtml", model];
      };
      Post["/venue/newband"] = _ => {
        Venue newVenue = Venue.Find(Request.Form["venue"]);
        Band newBand = Band.Find(Request.Form["band-id"]);
        newVenue.AddBand(newBand);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Post["/band/newvenue"] = _ => {
        Band newBand = Band.Find(Request.Form["band"]);
        Venue newVenue = Venue.Find(Request.Form["venue-id"]);
        newBand.AddVenue(newVenue);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
      Get["/venue/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", SelectedVenue];
      };
      Patch["/venue/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["venue-name"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };
    }
  }
}
