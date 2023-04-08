/*
 * Example script of using the otbm2json library
 * Changes all tiles on a map to chessboard pattern in global coordinates
 */

const otbm2json = require("../otbm2json");

const WHITE_TILE = 406;
const BLACK_TILE = 407;

// Read the map data using the otbm2json library
const mapData = otbm2json.read("map.otbm");

// Go over all nodes
mapData.data.nodes.forEach(function(x) {

  x.features.forEach(function(x) {
    
    // Skip anything that is not a tile area
    if(x.type !== otbm2json.HEADERS.OTBM_TILE_AREA) return; 

    // For each tile area; go over all actual tiles
    x.tiles.forEach(function(x) {

      // Skip anything that is not a tile (e.g. house tiles)
      if(x.type !== otbm2json.HEADERS.OTBM_TILE) return; 

      // Create a chessboard pattern using bitwise operators
      // Replace the id property of each tile
  

    });

  });

});

const myObj = { "map": mapData };
const jsonString = JSON.stringify(myObj);
const fs = require('fs');
fs.writeFile('map.json', jsonString, (err) => {
    if (err) {
      console.log('Error writing file:', err);
    } else {
      console.log('File saved successfully!');
    }
  });
