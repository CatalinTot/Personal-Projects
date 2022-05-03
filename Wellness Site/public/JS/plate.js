const macros = document.querySelectorAll('.food');
const generatePlateBtn = document.querySelector('.generate-plate-btn');
const infoButton = document.querySelector('.fa-info-circle');
const infoButtonDiv = document.querySelector('.info-div');

infoButton.setAttribute('data-bs-toggle', 'popover');
infoButton.setAttribute('title', 'Important!');
infoButton.setAttribute('data-bs-content', 'The data can be inaccurate! Feel free to ask the coach '+
' for a detailed meals plan.');

infoButtonDiv.addEventListener('mouseenter',()=>{
   infoButton.classList.remove('fa-info-circle-animation');
    
})
infoButtonDiv.addEventListener('mouseleave',()=>{
   infoButton.classList.add('fa-info-circle-animation');
    
})

generatePlateBtn.addEventListener('click', initPlate);

    
function initPlate(){
    // reload = location.reload();
    const randomProtein = Math.floor(Math.random() * proteins.length);
    const randomCarbs = Math.floor(Math.random() * carbs.length);
    const randomVegetable = Math.floor(Math.random() * vegetables.length);
    // const randomFat = Math.floor(Math.random()* fats.length);
    macros[0].style.backgroundImage = `url(${proteins[randomProtein]["imageSource:"]})`;
    addPopover(macros[0], randomProtein, 'protein');
    macros[1].style.backgroundImage = `url(${carbs[randomCarbs]["imageSource:"]})`;
    addPopover(macros[1], randomCarbs, 'carbs');
    macros[2].style.backgroundImage = `url(${vegetables[randomVegetable]["imageSource:"]})`;
    addPopover(macros[2], randomVegetable, 'vegetables');
    setTimeout(loadPopovers, 500);
}

function addPopover(element, number, macro) {
    
    let proteinSource,carbsSource,fiberSource;
    element.setAttribute('data-bs-toggle', 'popover');
    if (macro === 'protein') {
        element.setAttribute('title', proteins[number].proteinName);
        console.log('asfgadfhsdfh')
        proteinSource = computeProteinsForPlate(number);
        element.setAttribute('data-bs-content', 'Base on your needs, you can add ' +  proteinSource.proteinPerMeal + 'g of ' 
        +proteins[number].proteinName
        +'.This will bring you ' + proteinSource.amoutOfProtein + ' g of proteins, ' + proteinSource.amountOfCarbs + ' g of carbs, ' + proteinSource.amoutOfFats
        +' g of fats ' + proteinSource.amountOfFibers + ' g of fibers in ' + proteinSource.amoutOfCalories + ' calories');
    } else if (macro === 'carbs') {
        element.setAttribute('title', carbs[number].carbName);
        carbsSource = computeCarbsForPlate(number);
        element.setAttribute('data-bs-content', 'Base on your needs, you can add ' + carbsSource.carbsPerMeal + 'g of ' +
            carbs[number].carbName +
            '.This will bring you ' + carbsSource.amountOfProtein + ' g of proteins, ' + carbsSource.amountOfCarbs + ' g of carbs, ' + carbsSource.amountOfFats +
            ' g of fats ' + carbsSource.amountOfFibers + ' g of fibers in ' + carbsSource.amountOfCalories + ' calories');
    } else {
        element.setAttribute('title', vegetables[number].vegetableName);
        fiberSource = computeFibersForPlate(number);
        element.setAttribute('data-bs-content', 'Base on your needs, you can add ' + fiberSource.fibersPerMeal + 'g of ' +
        vegetables[number].vegetableName +
            '.This will bring you ' + fiberSource.amountOfProtein + ' g of proteins, ' + fiberSource.amountOfCarbs + ' g of carbs, ' + fiberSource.amountOfFats +
            ' g of fats ' + fiberSource.amountOfFibers + ' g of fibers in ' + fiberSource.amountOfCalories + ' calories');

    }
    element.setAttribute('data-bs-trigger', 'focus');
    element.setAttribute('tabindex', 0);
}

function computeProteinsForPlate(number) {
    const values = {};
    // the amout of protein source
    values.proteinPerMeal = Math.floor(((0.25 * gramsProteins) / proteins[number].nutrients.proteins) * 100);
    // the amount of protein in the protein source
    values.amoutOfProtein = Math.floor(0.25 * gramsProteins);
    // the amout of carbs in the protein source 
    values.amountOfCarbs = Math.floor((values.proteinPerMeal * proteins[number].nutrients.carbs) / 100);
    // the amount of fats in the protein source
    values.amoutOfFats = Math.floor((values.proteinPerMeal * proteins[number].nutrients.fats) / 100);
    // the amount of calories in the protein source
    values.amoutOfCalories = Math.floor((values.proteinPerMeal * proteins[number].nutrients.calories) / 100);
    // the amout if fibers in the protein source
    values.amountOfFibers = Math.floor((values.proteinPerMeal * proteins[number].nutrients.fibers) / 100);
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;
}

function computeCarbsForPlate(number){
    const values = {};
    // the amout of protein source
    values.carbsPerMeal = Math.floor(((0.25 * gramsCarbs) / carbs[number].nutrients.carbs) * 100);
    // the amount of protein in the protein source
    values.amountOfCarbs = Math.floor(0.25 * gramsCarbs);
    // the amout of carbs in the protein source 
    values.amountOfProtein = Math.floor((values.carbsPerMeal * carbs[number].nutrients.proteins) / 100);
    // the amount of fats in the protein source
    values.amountOfFats = Math.floor((values.carbsPerMeal * carbs[number].nutrients.fats) / 100);
    // the amount of calories in the protein source
    values.amountOfCalories = Math.floor((values.carbsPerMeal * carbs[number].nutrients.calories) / 100);
    // the amout if fibers in the protein source
    values.amountOfFibers = Math.floor((values.carbsPerMeal * carbs[number].nutrients.fibers) / 100);
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;
}

function computeFibersForPlate(number) {
    const values = {};
    // the amout of protein source
    values.fibersPerMeal = 300;
    // the amount of protein in the protein source
    values.amountOfFibers = Math.floor((300 * vegetables[number].nutrients.fibers) / 100);
    // the amout of carbs in the protein source 
    values.amountOfProtein = Math.floor((values.fibersPerMeal * vegetables[number].nutrients.proteins) / 100);
    // the amount of fats in the protein source
    values.amountOfFats = Math.floor((values.fibersPerMeal * vegetables[number].nutrients.fats) / 100);
    // the amount of calories in the protein source
    values.amountOfCalories = Math.floor((values.fibersPerMeal * vegetables[number].nutrients.calories) / 100);
    // the amout if fibers in the protein source
    values.amountOfCarbs = Math.floor((values.fibersPerMeal * vegetables[number].nutrients.carbs) / 100);
    for (let property in values) {
        if (isNaN(values[property]) === true || isFinite(values[property]) === false) {
            values[property] = 0;
        }
    }
    return values;
}