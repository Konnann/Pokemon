﻿using PokemonDB.Data.Migrations.Seed;

namespace Pokemon.Data
{
    public static class Utils
    {
        public static void InitDB()
        {
            using (var context = new PokemonContext())
            {
                context.Database.Initialize(true);
                //SeedManager.SeedTypes(context);
                //SeedManager.SeedSkills(context);
                //SeedManager.SeedPokedexEntires(context);
                //SeedManager.SeedPokemon(context);
                //SeedManager.SeedUsers(context);
                //SeedManager.SeedTrainers(context);
            }
        }
    }
}
