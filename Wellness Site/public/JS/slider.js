const sliders = document.querySelectorAll('.slider'); //select all the sliders on the page
const editButtons = document.querySelectorAll('.edit-btn'); //select all the edit buttons on the page
const deleteButtons = document.querySelectorAll('.delete-btn') //select all the delete buttons on the page
const editForm = document.querySelector('#result-edit-form'); //select the edit form
const deleteForm = document.querySelector('#result-delete-form'); //select the delete form
const inputFileCreate = document.querySelector('.result-input-file-create'); //input type file for creating the result
const inputFileEdit = document.querySelector('.result-input-file-edit'); //input type file for editing the result

// add an event listener for every slider on the page
for (let i = 0; i < sliders.length; i++) {
    sliders[i].addEventListener('input', (e) => {
        const sliderPos = e.target.value; //the value of current slider
        console.log(e.target);
        const siblings = sliders[i].parentElement.children; //the siblings of the current slider
        siblings[2].style.width = `${sliderPos}%`; //foreground image width
        siblings[4].style.left = `calc(${sliderPos}% - 18px)`; //slider button position

    })
}

// add an event listener for each edit button of the results
for (let i = 0; i < editButtons.length; i++) {
    editButtons[i].addEventListener('click', () => {
        editForm.action = `/results/${editButtons[i].id}?_method=PUT`; //set the action type of the edit form
        const elementChildren = editButtons[i].parentElement.parentElement.nextElementSibling.children; 
        //prefill the form areas
        document.getElementById('result-edit-name').value = elementChildren[0].innerHTML; 
        document.getElementById('result-edit-age').value = parseInt(elementChildren[2].children[0].innerHTML);
        document.getElementById('result-edit-result').value = elementChildren[1].children[0].innerHTML;
        // set the button for editing the result if we have 0 images
        if (inputFileEdit.files.length === 0) {
            inputFileEdit.parentElement.parentElement.nextElementSibling.children[0].disabled = false;
        }
        if (inputFileEdit.files.length !== 0) {
            inputFileEdit.parentElement.parentElement.nextElementSibling.children[0].disabled = true;
        }

    })
}

// set an event lister for each delete buttons of each result
for (let i = 0; i < deleteButtons.length; i++) {
    deleteButtons[i].addEventListener('click', () => {
        deleteForm.action = `/results/${deleteButtons[i].id}?_method=DELETE` //set the type of action of the delete form
    })
}

// live update of the submit result buttons availability if we have 2 photos
inputFileCreate.addEventListener('change', () => {
    if (inputFileCreate.files.length === 2) {
        inputFileCreate.parentElement.parentElement.nextElementSibling.children[0].disabled = false;
    }
    if (inputFileCreate.files.length !== 2) {
        inputFileCreate.parentElement.parentElement.nextElementSibling.children[0].disabled = true;
    }
})

// live update of the edit result buttons availability if we have 2 photos
inputFileEdit.addEventListener('change', () => {
    if (inputFileEdit.files.length === 2) {
        inputFileEdit.parentElement.parentElement.nextElementSibling.children[0].disabled = false;
    }
    if (inputFileEdit.files.length !== 2) {
        inputFileEdit.parentElement.parentElement.nextElementSibling.children[0].disabled = true;
    }
})