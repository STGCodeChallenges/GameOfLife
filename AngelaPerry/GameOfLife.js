<!--
function addClickListener(element, effect) { //Add click listener or event to specified element
	if (element.addEventListener) {
		element.addEventListener("click", effect);
	} else if (element.attachEvent) {
		element.attachEvent("click", effect);				
	} else {
		element.onclick = effect;				
	}
}

function removeClickListener(element, effect) { //Remove click listener or event from specified element
	if (element.removeEventListener) {
		element.removeEventListener("click", effect);
	} else if (element.detachEvent) {
		element.detachEvent("click", effect);				
	} else {
		element.onclick = null;				
	}
}

function addChangeListener(element, effect) { //Add change listener or event to specified element
	if (element.addEventListener) {
		element.addEventListener("change", effect);
	} else if (element.attachEvent) {
		element.attachEvent("change", effect);				
	} else {
		element.onchange = effect;				
	}
}

function createGrid() {
	//Lock table width and height
	var ltdwidth = x - 100;
	var actualcell = cellsize + 3;
	var tablewidth = Math.floor(ltdwidth/actualcell);
	var tableheight = Math.floor(y/actualcell);
	document.getElementById("grid").width = tablewidth * actualcell;
	document.getElementById("grid").height = tableheight * actualcell;

	//Create grid to fill window
	midpointx = Math.floor(tablewidth/2);
	midpointy = Math.floor(tableheight/2);
	
	var tbody = document.createElement("TBODY"); //Apparently TBODY is required before Chrome will assign row indexes to dynamically created rows.
	document.getElementById("grid").appendChild(tbody);
	
	for (var j = 0; j < tableheight; j++) {
		var gridrow = document.createElement("TR");
		
		for (var i = 0; i < tablewidth; i++) {
			var gridcell = document.createElement("TD");
			if (document.getElementById("start").value == "Start") {
				addClickListener(gridcell, cellStatus);
			}
			gridcell.style.height = cellsize + "px";
			gridcell.style.width = cellsize + "px";
			gridcell.id = (-1 * (midpointx - i)) + "," + (midpointy - j);
			gridrow.appendChild(gridcell);
		}
		document.getElementById("grid").tBodies[0].appendChild(gridrow);
	}
}

function growCellSize() { //Increase size of cells in grid
	if(cellsize < maxcell) {
		cellsize++;
		redrawGrid();
	} else {
		alert("Maximum cell size is " + maxcell);
		cellsize = maxcell;
	}
	document.getElementById("size").value = cellsize;
}

function shrinkCellSize() { //Decrease size of cells in grid
	if(cellsize > mincell) {
		cellsize--;
		redrawGrid();
	} else {
		alert("Minimum cell size is " + mincell);
		cellsize = mincell;
	}	
	document.getElementById("size").value = cellsize;
}

function changeCellSize() { //Changes cell size based on text field input
	newsize = document.getElementById("size").value;
	if(newsize < cellsize) {
		cellsize = parseInt(newsize) + 1;
		shrinkCellSize();
	}
	if(newsize > cellsize) {
		cellsize = parseInt(newsize) - 1;
		growCellSize();
	}
}

function slowDown() { //Decrease speed of animation
	if(speed < maxdelay) {
		speed++;
	} else {
		alert("Maximum delay is " + maxdelay);
		speed = maxdelay;
	}
	document.getElementById("speed").value = speed;
}

function speedUp() { //Increase speed of animation
	if(speed > mindelay) {
		speed--;
	} else {
		alert("Minimum delay is " + mindelay);
		speed = mindelay;
	}
	document.getElementById("speed").value = speed;
}

function changeSpeed() { //Changes speed of animation based on text field input
	newspeed = document.getElementById("speed").value;
	if(newspeed < speed) {
		speed = parseInt(newspeed) + 1;
		speedUp();
	}
	if(newspeed > speed) {
		speed = parseInt(newspeed) - 1;
		slowDown();
	}
}

function repaintGrid() { //Mark existing cells on grid
	for (i = 0; i < alive.length; i++) {
		if (document.getElementById(alive[i])) {
			document.getElementById(alive[i]).className = "Alive";
		}
	}
}

function redrawGrid() { //Redraw grid
	tbody = document.getElementById("grid").tBodies[0];
	document.getElementById("grid").removeChild(tbody);
	createGrid();
	repaintGrid();
}

function resetGame() { //Refresh page	
	location.reload();
}

function cellStatus() { //Color cells based on being alive or dead
	if (this.className == null || this.className === "") {
		this.className = "Alive";
		alive[alive.length] = this.id;
	} else {
		this.className = "";
		var index = alive.indexOf(this.id);
		alive.splice(index, 1);
	}
}

function startGame () { //Disable interaction and start game
	stop = false;
	
	var cells = document.body.querySelectorAll("td");
	for (var i = 0; i < cells.length; i++) {
		removeClickListener(cells[i], cellStatus);
	}
	
	var btn = document.getElementById("start");
	btn.value = "Stop";	
	removeClickListener(btn, startGame);	
	addClickListener(btn, stopGame);
	
	playGame();
}

function stopGame () { //Interrupt loop and enable interaction
	stop = true;
	
	var cells = document.body.querySelectorAll("td");
	for (var i = 0; i < cells.length; i++) {
		addClickListener(cells[i], cellStatus);
	}

	var btn = document.getElementById("start");
	btn.value = "Start";	
	removeClickListener(btn, stopGame);
	addClickListener(btn, startGame);
}

function playGame() { //Create loop to determine each generation and update screen
	if (stop) {
		return;
	}
	
	var nextGen = [];
	
	for (i = 0; i < alive.length; i++) {
		var count = 0;
		xy = alive[i].split(",");
		for (j = parseInt(xy[1]) - 1; j <= parseInt(xy[1]) + 1; j++) {
			for (k = parseInt(xy[0]) - 1; k <= parseInt(xy[0]) + 1; k++) {
				var testcell = k + "," + j;
				if (alive.indexOf(testcell) !== -1 && testcell != alive[i]) { //Count living cells near living cell
					count++;
				}
				if (alive.indexOf(testcell) === -1 && nextGen.indexOf(testcell) === -1) { //Check dead cells near living cell for life
					var lazarus = 0;
					for (m = j - 1; m <= j + 1; m++) {
						for (n = k - 1; n <= k + 1; n++) {
							var testcell2 = n + "," + m;
							if (alive.indexOf(testcell2) !== -1 && testcell2 != testcell) {
								lazarus++;
							}
						}
					}
					//Any dead cell with exactly three live neighbours cells will come to life
					if (lazarus === 3) {
						if (document.getElementById(testcell)) {
							document.getElementById(testcell).className = "Alive";
						}
						nextGen[nextGen.length] = testcell;
					}
				}
			}
		}
		
		//Any live cell with fewer than two live neighbours dies
		//Any live cell with more than three live neighbours dies
		if (count < 2 || count > 3) {
			if (document.getElementById(alive[i])) {
				document.getElementById(alive[i]).className = "";
			}
		} else {
		//Any live cell with two or three live neighbours lives
			if (nextGen.indexOf(alive[i]) === -1) {
				nextGen[nextGen.length] = alive[i];
			}
		}
	}
	
	alive = nextGen.slice(0);
	
	setTimeout(playGame, speed);
}

var alive = [];
var stop = false;

//Get size of window
var w = window,
	d = document,
	e = d.documentElement,
	g = d.getElementsByTagName('body')[0],
	x = w.innerWidth || e.clientWidth || g.clientWidth,
	y = w.innerHeight|| e.clientHeight|| g.clientHeight;

var mincell = 1 //Math.floor(Math.sqrt(x * y / 15360)) - 3;
var maxcell = Math.floor((x < y ? x : y) / 3) - 3;
var cellsize = 5;

//Set speed
var speed = 500;
var mindelay = 1;
var maxdelay = 5000;

window.onload = function() {
	createGrid();
	
	//Add functions to buttons
	var btn = document.getElementById("start");
	addClickListener(btn, startGame);
	var btn2 = document.getElementById("reset");
	addClickListener(btn2, resetGame);
	var btn3 = document.getElementById("minussize");
	addClickListener(btn3, shrinkCellSize);
	var btn4 = document.getElementById("plussize");
	addClickListener(btn4, growCellSize);
	var btn5 = document.getElementById("faster");
	addClickListener(btn5, speedUp);
	var btn6 = document.getElementById("slower");
	addClickListener(btn6, slowDown);
	
	//Wire up text fields
	var txt = document.getElementById("size");
	txt.value = cellsize;
	addChangeListener(txt, changeCellSize);	
	var txt2 = document.getElementById("speed");
	txt2.value = speed;
	addChangeListener(txt2, changeSpeed);
}
-->
