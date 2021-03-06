﻿using System.Collections.Generic;
using DMod.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;

namespace Data.Services
{
    public class StarService: IStarService
    {
        private readonly IStarRepository _starRep;
        private ICollection<Star> _stars;
        private ICollection<Structure> _structures;

        public StarService(IStarRepository starRep)
        {
            _starRep = starRep;
        }

        public ICollection<Star> LoadStars()
        {
            if (_stars != null) return _stars;
            _stars = _starRep.LoadStars();
            return _stars;
        }

        public ICollection<Structure> LoadStructures()
        {
            if (_structures != null) return _structures;
            _structures = _starRep.LoadStructures();
            return _structures;
        }
    }
}
