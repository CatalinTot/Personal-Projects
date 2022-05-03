const proButton = document.querySelector('.pro-plan');
const standardButton = document.querySelector('.standard-plan');
const planContainer = document.querySelector('.plan-container');
const registerContainer = document.querySelector('.register-container');
const goBack = document.querySelector('.go-back-button');

proButton.addEventListener('click',()=>{
    planContainer.style.animation = 'planAnimationFadeOut 1s forwards ease-in-out'
    document.querySelector('.sub-type-input').value = 'premium';
    setTimeout(()=>{
        planContainer.classList.add('d-none');
        goBack.classList.remove('d-none');
        registerContainer.style.display = 'block';
        registerContainer.style.animation = 'registerAnimationFadeIn 1s forwards ease-in-out'
    },900);
})

goBack.addEventListener('click',()=>{
    registerContainer.style.animation = 'registerAnimationFadeOut 1s forwards ease-in-out'
    setTimeout(() => {
        planContainer.classList.remove('d-none');
        goBack.classList.add('d-none');
        registerContainer.style.display = 'none';
        planContainer.style.animation = 'planAnimationFadeIn 1s forwards ease-in-out'
    }, 900);
})

standardButton.addEventListener('click',()=>{
     document.querySelector('.sub-type-input').value = 'standard';
     planContainer.style.animation = 'planAnimationFadeOut 1s forwards ease-in-out'
     setTimeout(() => {
         planContainer.classList.add('d-none');
         goBack.classList.remove('d-none');
         registerContainer.style.display = 'block';
         registerContainer.style.animation = 'registerAnimationFadeIn 1s forwards ease-in-out'
     }, 900);
})