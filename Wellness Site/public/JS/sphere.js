let user = JSON.parse(currentUser);
let RMA, RMB, gramsProteins, kcalProteins, magicNumber, gramsCarbs, kcalCarbs, gramsFats, kcalFats, proteinsQuantity;
let userActivity = document.querySelector(".user-activity"); //the type of activity of the current user

// if the activity is changed, changed all the values
userActivity.addEventListener('change', async () => {
    document.querySelector('.scene').remove();
    setUserData();
    Draw();
    loadPopovers();
});

// when the content of the page is loaded, set the user data and draw the sphere
document.addEventListener('DOMContentLoaded', () => {
    setUserData();
    Draw();
    initPlate();
});

// set user data function
function setUserData() {
    if (user.gender === 'Male') {
        RMB = Math.floor(66.673 + (13.751 * user.weight) + (5.0033 * user.heigth) - (6.775 * user.age));
        if (userActivity.value === 'Active') {
            RMA = Math.floor(RMB * 1.3);
            if (user.age >= 18 && user.age <= 35) {
                proteinsQuantity = 1.5;
            } else if (user.age > 35 && user.age <= 45) {
                proteinsQuantity = 1.6;
            } else {
                proteinsQuantity = 1.8;
            }

        } else if (userActivity.value === 'Sedentary') {
            RMA = Math.floor(RMB + 300);
            if (user.age >= 18 && user.age <= 35) {
                proteinsQuantity = 1.3;
            } else if (user.age > 35 && user.age <= 45) {
                proteinsQuantity = 1.4;
            } else {
                proteinsQuantity = 1.4;
            }

        } else {
            RMA = Math.floor(RMB * 1.3);
            proteinsQuantity = 2;
        }
        gramsProteins = proteinsQuantity * user.weight;
        kcalProteins = gramsProteins * 4;


    } else if (user.gender === "Female") {
        RMB = Math.floor(665.51 + (9.563 * user.weight) + (1.8496 * user.heigth) - (4.6756 * user.age));
        if (userActivity.innerHTML === 'Active') {
            RMA = RMB * 1.25;
            if (user.age >= 18 && user.age <= 35) {
                proteinsQuantity = 1.4;
            } else if (user.age > 35 && user.age <= 45) {
                proteinsQuantity = 1.5;
            } else {
                proteinsQuantity = 1.6;
            }
        } else if (userActivity.innerHTML === 'Sedendary') {
            RMA = RMB + 250;
            if (user.age >= 18 && user.age <= 35) {
                proteinsQuantity = 1.2;
            } else if (user.age > 35 && user.age <= 45) {
                proteinsQuantity = 1.3;
            } else {
                proteinsQuantity = 1.4;
            }
        } else {
            RMA = RMB * 1.25;
            proteinsQuantity = 1.8;
        }
        gramsProteins = proteinsQuantity * user.weight;
        kcalProteins = gramsProteins * 4;

    }

    if (user.goal === "Weightloss") {
        magicNumber = RMA - 500;
    } else if (user.goal === "Maintain your weight") {
        magicNumber = RMA;
    } else {
        magicNumber = RMA + 500;
    }

    kcalFats = Math.floor(magicNumber * 0.3);
    gramsFats = Math.floor(kcalFats / 9);
    kcalCarbs = Math.floor(magicNumber - kcalProteins - kcalFats);
    gramsCarbs = Math.floor(kcalCarbs / 4);

    // fill the table with user data
    document.querySelector('.magic-number-value').innerHTML = magicNumber + ' ' + 'kcal';
    document.querySelector('.proteins-value-grams').innerHTML = gramsProteins + ' ' + 'g';
    document.querySelector('.proteins-value-kcal').innerHTML = kcalProteins + ' ' + 'kcal';
    document.querySelector('.fats-value-grams').innerHTML = gramsFats + ' ' + 'g';
    document.querySelector('.fats-value-kcal').innerHTML = kcalFats + ' ' + 'kcal';
    document.querySelector('.carbs-value-grams').innerHTML = gramsCarbs + ' ' + 'g';
    document.querySelector('.carbs-value-kcal').innerHTML = kcalCarbs + ' ' + 'kcal';
    document.querySelector('.hydration-value').innerHTML = user.weight * 50 + ' ' + 'l';
}

let Page = (function () {

    'use strict';
    // rotate the sphere using the arrows of the heyboard
    let rotateControls = function (sphere) {
        let initialValue = 0;
        window.addEventListener('keydown', e => {
            if (e.key === 'ArrowUp') {
                initialValue -= 45;
                e.preventDefault();
                sphere.sphereAnimation('stop');
                sphere.sphereTransforms('rotateX', initialValue);
            }
            if (e.key === 'ArrowDown') {
                initialValue += 45;
                e.preventDefault();
                sphere.sphereAnimation('stop');
                sphere.sphereTransforms('rotateX', initialValue);
            }
            if (e.key === 'ArrowLeft') {
                initialValue -= 45;
                e.preventDefault();
                sphere.sphereAnimation('stop');
                sphere.sphereTransforms('rotateY', initialValue);
            }
            if (e.key === 'ArrowRight') {
                initialValue += 45;
                e.preventDefault();
                sphere.sphereAnimation('stop');
                sphere.sphereTransforms('rotateY', initialValue);
            }

        });

    };

    //return the state of the spehere
    return {
        Init: {
            rotateControls: rotateControls
        },
    };

}());

// draw the sphere
function Draw() {


    let sphere = new Sphere(document.getElementById('Draw3dSphere'), {
        sphereClass: 'circles',
        animation: 'translateZ rotateY'
    });
    if (!sphere.ok()) {
        let nope = document.getElementById('NoSupport');
        nope.innerHTML += '<pre>Missing HTML5 support: ' + sphere.errors.join(', ') + '</pre>';
        nope.style.display = 'block';
    }

    Page.Init.rotateControls(sphere);

    let sphereStop = document.querySelector('.sphere');
    let scene = document.querySelector('#Draw3dSphere');
    scene.addEventListener('mouseenter', () => {
        sphereStop.style.animationPlayState = 'paused';
    })
    scene.addEventListener('mouseleave', () => {
        sphereStop.style.animationPlayState = 'running';
    })
}

let Style = {
    el: null,
    props: {},
    prefixes: ['ms', 'Moz', 'Webkit'], //opera is now Webkit

    test: function (prop) {
        if (!this.el) {
            this.el = document.createElement('div');
        }
        if (typeof this.el.style[prop] !== 'undefined') {
            this.props[prop] = prop;
            return prop;
        }
        let slug = prop[0].toUpperCase() + prop.slice(1);
        let _prop;
        for (let i = 0; i < this.prefixes.length; i++) {
            _prop = this.prefixes[i] + slug;
            if (typeof this.el.style[_prop] !== 'undefined') {
                this.props[prop] = _prop;
                return _prop;
            }
        }
        this.props[prop] = false;

        return false;
    },

    // ie < edge test
    supportsPreserve3d: function () {
        if (!this.test('transformStyle')) {
            return false;
        }
        let prop = this.prefix('transformStyle');
        let el = document.createElement('div');
        el.style[prop] = 'preserve-3d';
        // browsers set only EUNUM (valid) values
        return typeof el.style[prop] !== 'undefined' && el.style[prop] === 'preserve-3d';
    },

    // get prefix if if exist, test fails the original prop will be returned
    prefix: function (prop) {
        let test;
        if (typeof Style.props[prop] === 'undefined') {
            test = Style.test(prop);
        }
        test = Style.props[prop];
        return (test) ? test : prop;
    }
};

//shorthand
let prefix = Style.prefix;

let Sphere = function (wrapper, options) {

    // the default values for the sphere
    this.defaults = function () {
        return {
            columns: 12,
            rows: 6,
            radius: 250,
            perspective: 800,
            translateX: 0,
            translateY: 0,
            translateZ: 0,
            rotateX: 0,
            rotateY: 0,
            rotateZ: 0,
            animation: 'rotateY',
            sphereClass: null,
            containerClass: null,
            poleCaps: [90, 270] //pole caps (degree) .5* PI, 1.5 * PI;
        };
    };

    this.state = this.defaults();

    this.debug = false;
    this.errors = [];

    let css3 = ['transform', 'transformStyle', 'transition', 'backfaceVisibility', 'perspective'];
    for (let i = 0; i < css3.length; i++) {
        if (!Style.test(css3[i])) {
            this.errors.push(css3[i]);
        }
    }
    if (!Style.supportsPreserve3d()) {
        this.errors.push('transformStyle: preserve-3d');
    }

    let cache = {};
    this.cache = {
        get: function (key, _default) {
            _default = _default || null;
            return (cache.hasOwnProperty(key)) ? cache[key] : _default;
        },
        set: function (key, value) {
            let type = Object.prototype.toString.call(value);
            if (type === '[object Object]' || type === '[object Array]') {
                value = JSON.parse(JSON.stringify(value));
            }
            cache[key] = value;
        },
        purge: function (key) {
            key = key || false;
            if (key) {
                delete(cache[key]);
            } else {
                cache = {};
            }
        },
    };

    this.merge(this.state, options || {});
    this.cache.set('initState', this.state);

    this.nodes = {
        wrapper: wrapper,
        scene: document.createElement('div'),
        sphere: document.createElement('div'),
        container: document.createElement('div'),
        centre: document.createElement('div'),
        columns: []
    };

    this.nodes.scene.className = 'scene';
    this.nodes.sphere.className = 'sphere';
    this.nodes.container.className = 'container';
    this.nodes.centre.className = 'sphere-centre';

    this.nodes.wrapper.appendChild(this.nodes.scene);
    this.nodes.scene.appendChild(this.nodes.sphere);
    this.nodes.sphere.appendChild(this.nodes.container);
    this.nodes.container.appendChild(this.nodes.centre);

    if (this.ok(true)) {
        this.draw();
    }

};

Sphere.prototype.ok = function (warn) {
    warn = warn || false;
    if (this.errors.length) {
        if (warn) {
            console.warn('css3sphere: not supported by this browser: ' + this.errors.join(', '));
        }
        return false;
    }
    return true;
};

Sphere.prototype.merge = function (state, options) {
    for (let key in options) {
        if (options.hasOwnProperty(key)) {
            state[key] = options[key];
        }
    }
    return state;
};


Sphere.prototype.nodeClasses = function (node, action, classes) {
    if (!classes) {
        return false;
    }

    if (typeof node === 'string') {
        node = (typeof this.nodes[node] !== 'undefined') ? this.nodes[node] : null;
    }

    if (typeof node.classList === 'undefined') {
        return false;
    }

    classes = classes.split(' ');
    for (let i = 0; i < classes.length; i++) {
        if (!classes[i]) {
            continue;
        }
        switch (action) {
            case 'add':
                node.classList.add(classes[i]);
                break;
            case 'remove':
                node.classList.remove(classes[i]);
                break;
            case 'toggle':
                node.classList.toggle(classes[i]);
                break;
            default:
                return false;
        }
    }
    return node.className;
};

Sphere.prototype.reset = function () {
    this.state = (typeof this.cache.initState !== 'undefined') ? this.cache.initState : this.defaults();
    this.draw();
};

// update the state of the sphere
Sphere.prototype.updateState = function (values) {
    if (Object.prototype.toString.call(values) !== '[object Object]') {
        return false;
    }

    let reference = this.defaults();
    let refType;
    let valType;

    // type casting to defaults
    for (let key in values) {
        if (!values.hasOwnProperty(key)) {
            continue;
        }
        if (!reference.hasOwnProperty(key)) {
            continue;
        }

        refType = Object.prototype.toString.call(reference[key]);
        valType = Object.prototype.toString.call(values[key]);

        if (valType === refType && refType !== '[object Number]') {
            continue;
        }

        if (refType === '[object Number]') {
            values[key] = parseInt(values[key], 10);
            if (isNaN(values[key])) {
                delete(values[key]);
            }
        }
    }

    this.merge(this.state, values);
    this.draw(this.nodes.columns);
};

// draw the sphere
Sphere.prototype.draw = function (contents) {

    if (!this.ok()) {
        return;
    }

    contents = contents || [];
    // console.log(contents);
    this.nodes.columns = [];
    this.nodes.container.innerHTML = '';

    this.nodes.scene.style.minWidth = (2 * this.state.radius) + 'px';
    this.nodes.scene.style.minHeight = (2 * this.state.radius) + 'px';
    this.nodes.container.appendChild(this.nodes.centre);
    let rotate = {
        row: 360 / this.state.columns,
        column: (360 / 2) / this.state.rows
    };

    let translate = {
        z: (this.state.radius / 2) / Math.tan(rotate.column * Math.PI / 180)
    };

    let column;
    let content;
    let counter = 0;
    // create the element for each index
    for (let r = 0; r < this.state.rows; r++) {

        for (let c = 0; c < this.state.columns; c++) {
            column = this.renderColumn(r, c, rotate, translate, food[counter]);
            counter++;
            if (counter === 19)
                counter = 0;
            if (column) {
                this.nodes.container.appendChild(column);
                if (contents.length) {
                    content = contents.shift();
                    this.columnContent(content, this.nodes.columns.length - 1);
                }
            }
        }
    }

    this.nodes.scene.style[prefix('perspective')] = this.state.perspective + 'px';

    this.containerClass();
    this.sphereClass();
    this.sphereTransforms();
    this.sphereAnimation();
};

// create the column of the sphere
Sphere.prototype.renderColumn = function (rowIndex, columnIndex, _rotate, translate, foodItem) {
    let rotate = {
        x: rowIndex * _rotate.row,
        y: columnIndex * _rotate.column
    };

    if (!this.isDisplayableColumn(rowIndex, rotate.y)) {
        return false;
    }

    let column = document.createElement('div');
    column.className = 'sphere-column row-' + rowIndex + ' col-' + columnIndex;
    column.style[prefix('transform')] = 'rotateX(' + rotate.x + 'deg) rotateY(' + rotate.y + 'deg) translateZ(' + Math.round(translate.z) + 'px)';


    let cell = this.renderColumnCell(rowIndex, foodItem);
    column.appendChild(cell);

    if (this.debug) {
        cell.innerText = 'row ' + rowIndex + ' col ' + columnIndex;
    }

    return column;
};

// render each cell of each column
Sphere.prototype.renderColumnCell = function (rowIndex, foodItem) {
    let proteinSource, carbsSource, fibersSource, totalNutrients = {};
    let cell = document.createElement('div');
    let img = document.createElement('img');
    img.classList.add('food-img');
    img.setAttribute('data-bs-toggle', 'popover');
    img.setAttribute('title', foodItem.foodTitle);
    img.setAttribute('data-bs-trigger', 'focus');
    img.setAttribute('tabindex', 0);
    // due to the time you should eat the current food, calculate the macros
    if (foodItem.whenToEat === 'Lunch') {
        proteinSource = computeProteins(foodItem, 0.25);
        carbsSource = computeCarbs(foodItem, 0.25);
        fibersSource = computeFibers(foodItem);
        fatsSource = computeFats(foodItem, 0.05);
        totalNutrients = computeNutrients(proteinSource, carbsSource, fibersSource, fatsSource);

    } else if (foodItem.whenToEat === 'Dinner') {
        proteinSource = computeProteins(foodItem, 0.25);
        carbsSource = computeCarbs(foodItem, 0.25);
        fibersSource = computeFibers(foodItem);
        fatsSource = computeFats(foodItem, 0.05);
        totalNutrients = computeNutrients(proteinSource, carbsSource, fibersSource, fatsSource);
    } else {
        proteinSource = computeProteins(foodItem, 0.05);
        carbsSource = computeCarbs(foodItem, 0.05);
        fibersSource = computeFibers(foodItem);
        fatsSource = computeFats(foodItem, 0.05);
        totalNutrients = computeNutrients(proteinSource, carbsSource, fibersSource, fatsSource);
    }
    // render each column cell with a photo, and add a popover with the correct quantity for each users
    if (foodItem.whenToEat === 'Lunch' || foodItem.whenToEat === 'Dinner') {
        img.setAttribute('data-bs-content', 'Base on your needs, you can add ' + totalNutrients.totalCarbsQuantity + 'g of ' + foodItem.carbsSource.name +
            ', 300g of ' + foodItem.vegetableSource.name + ',' + totalNutrients.totalProteinSource + 'g of ' + foodItem.proteinSource.name + ' and ' +
            totalNutrients.totalFatsSource + ' g of ' + foodItem.fatsSource.name + '.This will add up to ' + totalNutrients.totalProteins + ' g of proteins, ' + totalNutrients.totalCarbs + ' g of carbs, ' + totalNutrients.totalFats +
            ' g of fats ' + totalNutrients.totalFibers + ' g of fibers in ' + totalNutrients.totalCalories + ' calories');
    } else if (foodItem.whenToEat === 'Breakfast') {
        img.setAttribute('data-bs-content', 'On a smoothie based breaksfast you will find ' + foodItem.proteinSource.nutrients.gramsProteins + ' g of proteins ' +
            foodItem.proteinSource.nutrients.Carbs + ' g of carbs ' + foodItem.proteinSource.nutrients.Fibers + ' g of fibers ' + ' in ' +
            foodItem.proteinSource.nutrients.Calories + ' calories');
    } else {
        if (foodItem.carbsSource.name === 'none') {
            img.setAttribute('data-bs-content', 'Base on your needs, you can add ' + totalNutrients.totalFatsSource + ' g of ' + foodItem.fatsSource.name +
                ' and ' + totalNutrients.totalProteinSource + 'g of ' + foodItem.proteinSource.name +
                '.This will add up to ' + totalNutrients.totalProteins + ' g of proteins, ' + totalNutrients.totalFats + ' g of fats, in ' + totalNutrients.totalCalories + ' calories');
        } else {
            img.setAttribute('data-bs-content', 'Base on your needs, you can add ' + totalNutrients.totalCarbsQuantity + ' g of ' + foodItem.carbsSource.name +
                ' and ' + totalNutrients.totalProteinSource + 'g of ' + foodItem.proteinSource.name +
                '.This will add up to ' + totalNutrients.totalProteins + ' g of proteins, ' + totalNutrients.totalCarbs + ' g of carbs, in ' + totalNutrients.totalCalories + ' calories');
        }

    }

    img.src = foodItem.imageSource;
    cell.append(img);
    let index = this.nodes.columns.push(cell);
    cell.className = 'column-content';
    cell.dataset.cellIndex = index;
    return cell;
};

Sphere.prototype.isDisplayableColumn = function (rowIndex, columnYangle) {

    let isPoleCap = (this.state.poleCaps.indexOf(columnYangle) > -1);

    if (isPoleCap && rowIndex !== 0) {
        return false;
    }

    let isPolar = false;
    for (let i = 0; i < this.state.poleCaps.length; i++) {
        isPolar = (Math.abs(this.state.poleCaps[i] - columnYangle) <= 30);
        if (isPolar) {
            break;
        }
    }

    if (isPolar && (rowIndex % 2)) {
        return false;
    }

    return true;
};

Sphere.prototype.sphereTransforms = function (property, value) {

    let state = this.state;

    if (typeof property !== 'undefined' && this.state.hasOwnProperty(property)) {
        state[property] = value;
    }

    let styles = [];
    let transform = function () {
        let unit = arguments[0];
        let r = [];
        for (let i = 1; i < arguments.length; i++) {
            r.push(arguments[i] + '(' + state[arguments[i]] + unit + ')');
        }
        return r;
    };

    styles = styles.concat(transform('px', 'translateX', 'translateY', 'translateZ'));
    styles = styles.concat(transform('deg', 'rotateX', 'rotateY', 'rotateZ'));

    if (styles.length) {
        this.nodes.container.style[prefix('transform')] = styles.join(' ');
    }

    return true;
};

// animate the sphere
Sphere.prototype.sphereAnimation = function (animation) {
    // console.log(this.state.animation);

    if (typeof animation === 'undefined') {
        animation = this.state.animation;
    }

    if (this.state.animation && animation === 'stop') {
        this.nodeClasses('sphere', 'add', 'paused');
        return;
    }

    if (this.state.animation && animation === 'play') {
        this.nodeClasses('sphere', 'remove', 'paused');
        return;
    }

    if (animation !== this.state.animation) {
        this.nodeClasses('sphere', 'remove', this.state.animation);
    }

    this.state.animation = animation;

    if (this.state.animation) {
        this.nodeClasses('sphere', 'add', this.state.animation);
    }

};

// 
Sphere.prototype.sphereClass = function (sphereClass) {

    if (this.state.sphereClass) {
        this.nodeClasses('sphere', 'remove', this.state.sphereClass);
    }

    if (typeof sphereClass !== 'undefined') {
        this.state.sphereClass = sphereClass;
    }

    if (this.state.sphereClass) {
        this.nodeClasses('sphere', 'add', this.state.sphereClass);
    }
};

Sphere.prototype.containerClass = function (containerClass) {

    if (this.state.containerClass) {
        this.nodeClasses('container', 'remove', this.state.containerClass);
    }

    if (typeof containerClass !== 'undefined') {
        this.state.containerClass = containerClass;
    }

    if (this.state.containerClass) {
        this.nodeClasses('container', 'add', this.state.containerClass);
    }
};

Sphere.prototype.columnContent = function (node, index) {
    // console.log(node)
    index = (typeof index === 'number') ? index : -1;
    let columns = this.nodes.columns;

    let addNode = function (index, node) {
        if (typeof columns[index] === 'undefined') {
            return -1;
        }

        while (columns[index].firstChild) {
            columns[index].removeChild(columns[index].firstChild);
        }

        if (typeof node === 'string') {
            columns[index].innerHTML = node;
        } else {
            columns[index].appendChild(node);
        }

        return index;
    };

    // single
    if (index > -1) {
        return addNode(index, node);
    }

    // all
    for (let i = 0; i < columns.length; i++) {
        addNode(i, node);
    }
    return columns.length - 1;
};

// wait for DOM to load, and then add popovers
setTimeout(loadPopovers, 500);

function loadPopovers() {
    let popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    })
}

window.Sphere = Sphere;

// compute proteins for each plate
function computeProteins(foodItem, percent) {
    let values = {};
    let proteinsFromProteins = foodItem.proteinSource.nutrients.gramsProteins; //the proteins from the protein source
    let carbsFromProteins = foodItem.proteinSource.nutrients.Carbs;
    let fatsFromProteins = foodItem.proteinSource.nutrients.Fats;
    let caloriesFromProteins = foodItem.proteinSource.nutrients.Calories;
    let fibersFromProteins = foodItem.proteinSource.nutrients.Fibers;

    values.proteinsSource = ((percent * gramsProteins) / proteinsFromProteins) * 100; //the grams of protein source
    values.proteinsQuantity = percent * gramsProteins;
    values.carbsFromProteins = (values.proteinsSource * carbsFromProteins) / 100;
    values.fatsFromProteins = (values.proteinsSource * fatsFromProteins) / 100;
    values.caloriesFromProteins = (values.proteinsSource * caloriesFromProteins) / 100;
    values.fibersFromProteins = (values.proteinsSource * fibersFromProteins) / 100;
    // if there are Nan of Infinity values, turn them into 0 because we do not need them
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;
}

// compute carbs for each plate
function computeCarbs(foodItem, percent) {
    let values = {};
    let proteinsFromCarbs = foodItem.carbsSource.nutrients.gramsProteins; //the proteins from the protein source
    let carbsFromCarbs = foodItem.carbsSource.nutrients.Carbs;
    let fatsFromCarbs = foodItem.carbsSource.nutrients.Fats;
    let caloriesFromCarbs = foodItem.carbsSource.nutrients.Calories;
    let fibersFromCarbs = foodItem.carbsSource.nutrients.Fibers;

    values.carbsSource = ((percent * gramsCarbs) / carbsFromCarbs) * 100;
    values.carbsQuantity = percent * gramsCarbs;
    values.proteinsFromCarbs = (values.carbsSource * proteinsFromCarbs) / 100;
    values.fatsFromCarbs = (values.carbsSource * fatsFromCarbs) / 100;
    values.caloriesFromCarbs = (values.carbsSource * caloriesFromCarbs) / 100;
    values.fibersFromCarbs = (values.carbsSource * fibersFromCarbs) / 100;
    // if there are Nan of Infinity values, turn them into 0 because we do not need them
    for (let property in values) {
        if(isNaN(values[property]) === true || isFinite(values[property]) ===false){
            values[property] =0;
        }
    }
    
    return values;

}

// compute fibers for each plate
function computeFibers(foodItem) {
    let values = {};
    let carbsFromVegetables = foodItem.vegetableSource.nutrients.Carbs; //the proteins from the protein source
    let proteinsFromVegetalbes = foodItem.vegetableSource.nutrients.gramsProteins;
    let fatsFromVegetables = foodItem.vegetableSource.nutrients.Fats;
    let caloriesFromVegetables = foodItem.vegetableSource.nutrients.Calories;
    let firbersFromVegetables = foodItem.vegetableSource.nutrients.Fibers

    values.vegetableSource = 300;
    values.fibersQuantity = (300 * firbersFromVegetables) / 100;
    values.proteinsFromVegetables = (300 * proteinsFromVegetalbes) / 100;
    values.fatsFromVegetables = (300 * fatsFromVegetables) / 100;
    values.caloriesFromVegetables = (300 * caloriesFromVegetables) / 100;
    values.carbsFromVegetables = (300 * carbsFromVegetables) / 100;
    // if there are Nan of Infinity values, turn them into 0 because we do not need them
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;

}

// compute the total fats
function computeFats(foodItem, percent) {
    let values = {};
    let carbsFromFats = foodItem.fatsSource.nutrients.Carbs; //the proteins from the protein source
    let proteinsFromFats = foodItem.fatsSource.nutrients.gramsProteins;
    let fatsFromFats = foodItem.fatsSource.nutrients.Fats;
    let caloriesFromFats = foodItem.fatsSource.nutrients.Calories;
    let fibersFromFats = foodItem.fatsSource.nutrients.Fibers

    values.fatsSource = ((percent * gramsFats) / fatsFromFats) * 100;
    values.fatsQuantity = percent * gramsFats;
    values.proteinsFromFats = (values.fatsSource * proteinsFromFats) / 100;
    values.carbsFromFats = (values.fatsSource * carbsFromFats) / 100;
    values.caloriesFromFats = (values.fatsSource * caloriesFromFats) / 100;
    values.fibersFromFats = (values.fatsSource * fibersFromFats) / 100;
    // if there are Nan of Infinity values, turn them into 0 because we do not need them
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;

}

// round the values of each nutrinet
function computeNutrients(proteinSource, carbsSource, fibersSource, fatsSource) {
    let values = {};
    values.totalProteinSource = Math.floor(proteinSource.proteinsSource);
    values.totalProteins = Math.floor(proteinSource.proteinsQuantity + carbsSource.proteinsFromCarbs + fibersSource.proteinsFromVegetables + fatsSource.proteinsFromFats);
    values.totalCarbsQuantity = Math.floor(carbsSource.carbsQuantity);
    values.totalCarbs = Math.floor(proteinSource.carbsFromProteins + carbsSource.carbsQuantity + fibersSource.carbsFromVegetables + fatsSource.carbsFromFats);
    values.totalFiberSource = Math.floor(fibersSource.vegetableSource);
    values.totalFibers = Math.floor(proteinSource.fibersFromProteins + carbsSource.fibersFromCarbs + fibersSource.fibersQuantity + fatsSource.fibersFromFats);
    values.totalFatsSource = Math.floor(fatsSource.fatsSource);
    values.totalFats = Math.floor(proteinSource.fatsFromProteins + carbsSource.fatsFromCarbs + fibersSource.fatsFromVegetables + fatsSource.fatsQuantity);
    values.totalCalories = Math.floor(proteinSource.caloriesFromProteins + carbsSource.caloriesFromCarbs + fibersSource.caloriesFromVegetables + fatsSource.caloriesFromFats);

    return values;
}