﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using AutoMapper.Internal;
using System.Web.Http.Controllers;
using ArtMuseum.Services;
using ArtMuseum.Models;
using ArtMuseum.Data;

namespace ArtMuseum.API.Controllers
{
    [Authorize]
    public class MuseumController : ApiController
    {
        private readonly ApplicationDbContext _collection = new ApplicationDbContext();

        private MuseumServices CreateMuseumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var museumService = new MuseumServices(userId);
            return museumService;
        }

        [HttpGet]//Get all museums
        public IHttpActionResult GetAll()
        {
            MuseumServices museumServices = CreateMuseumService();
            var museums = museumServices.GetMuseums();
            return Ok(museums);
        }

        [HttpGet]//Get by id
        [Route("id")]
        public IHttpActionResult Get(int id)
        {
            MuseumServices museumServices = CreateMuseumService();
            var museum = museumServices.GetMuseumById(id);
            return Ok(museum);
        }

        [HttpGet]//Get by name
        [Route("name")]
        public IHttpActionResult Get(string name)
        {
            MuseumServices museumServices = CreateMuseumService();
            var museum = museumServices.GetMuseumByName(name);
            return Ok(museum);
        }

        //GET Collection
        [HttpGet]
        [Route("museum-collection")]
        public IHttpActionResult GetCollection(int museum)
        {
            MuseumServices museumServices = CreateMuseumService();
            var artworks = museumServices.GetArtworksAtMuseum(museum);
            return Ok(artworks);
        }

        //Get Employee Roster
        [HttpGet]
        [Route("museum-roster")]
        public IHttpActionResult GetRoster(int museum)
        {
            MuseumServices museumServices = CreateMuseumService();
            var employees = museumServices.GetEmployeesAtMuseum(museum);
            return Ok(employees);
        }

        [HttpPost]//Create
        public IHttpActionResult Post(MuseumCreate museum)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMuseumService();

            if (!service.CreateMuseum(museum))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]//Edit
        public IHttpActionResult Put(MuseumEdit museum)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMuseumService();

            if (!service.UpdateMuseum(museum))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]//Delete
        public IHttpActionResult Delete(int id)
        {
            var service = CreateMuseumService();

            if (!service.DeleteMuseum(id))
                return InternalServerError();

            return Ok();
        }
    }
}
