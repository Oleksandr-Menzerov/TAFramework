Feature: Authorization
	I want to manage my authorization options
	so that I can change my password, sign up, and log in securely.

Background:
  Given I am on the main page

@Retry(3)
@UI
Scenario: Change the password
	As a user,
	I want to change my password
	so that I can reset it when needed.
  When I click the 'Акаунт' link
  And I click the 'Забули пароль?' link
  Then I should see the following elements
	| Type   | Name                                                                 |
	| label  | Забули пароль?                                                       |
	| label  | Не хвилюйтесь, ми відправимо Вам код для відновлення паролю на пошту |
	| input  | Електронна пошта                                                     |
	| button | Надіслати код                                                        |
  When I enter 'Maybe an email' in the input 'Електронна пошта'
  And I click the 'Забули пароль?' header
  Then the input 'Електронна пошта' should have color 'rgb(255, 0, 0)'
  And the input 'Електронна пошта' should be invalid
  And the button 'Надіслати код' should be disabled
  When I enter 'Maybe@email.com' in the input 'Електронна пошта'
  Then the input 'Електронна пошта' should have color 'rgb(73, 69, 79)'
  And the input 'Електронна пошта' should be valid
  And the button 'Надіслати код' should be enabled
  When I click the 'Надіслати код' button
  Then I should see a successful modal with header 'Посилання надіслано!' and description 'Посилання для зміни вашого паролю надіслано на Maybe@email.com.'
  #When I close the modal
  When I click out of the modal
  Then I should not see the modal
  Then I should see the following elements
	| Type   | Name                                                 |
	| label  | Створіть новий пароль                                |
	| label  | Встановіть новий пароль для вашого облікового запису |
	| input  | Пароль                                               |
	| input  | Повторіть пароль                                     |
	| button | Змінити пароль                                       |
  When I enter the following data in the inputs
    | Field            | Value                |
    | Пароль           | TheFirstPassWord123! |
    | Повторіть пароль | TheSecondOne321?     |
  Then the button 'Змінити пароль' should be disabled
  When I enter the following data in the inputs
    | Field            | Value               |
    | Пароль           | TheSamePassWord123! |
    | Повторіть пароль | TheSamePassWord123! |
  Then the button 'Змінити пароль' should be enabled
  When I click the 'Змінити пароль' button
  Then I should see a successful modal

  @UI
Scenario: Sign up
	As a user,
	I want to sign up
	so that I can create a new account.
  When I click the 'Акаунт' link
  When I click the 'Зареєструватися' link
  Then I should see the following elements
	| Type     | Name                                                 |
	| label    | Привіт!                                              |
	| label    | Будь ласка введіть свої дані, щоб зареєструватися    |
	| input    | Імʼя                                                 |
	| input    | Прізвище                                             |
	| dropdown | Локація                                              |
	| input    | Електронна пошта                                     |
	| input    | Пароль                                               |
	| input    | Повторіть пароль                                     |
	| checkbox | Я погоджуюся з                                       |
	| link     | Політикою конфіденційності та Правилами користування |
	| button   | Зареєструватися                                      |
	| label    | Вже маєте аккаунт?                                   |
	| link     | Вхід                                                 |
  And the button 'Зареєструватися' should be disabled
  When I enter the following data in the inputs
    | Field            | Value               |
    | Імʼя             | Auto                |
    | Прізвище         | Tester              |
  And I select the 'Харків' option in the dropdown 'Локація'
  And I enter the following data in the inputs
    | Field            | Value               |
    | Електронна пошта | newuser@e.mail      |
    | Пароль           | TheSamePassWord123! |
    | Повторіть пароль | TheSamePassWord123! |
  And I check the 'Я погоджуюся' checkbox
  Then the button 'Зареєструватися' should be enabled
  When I click the 'Зареєструватися' button
  Then I should see a successful modal

  @UI
Scenario: Log in
	As a user,
	I want to log in
	so that I can access my account.
  When I click the 'Акаунт' link
  Then I should see the following elements
	| Type     | Name                                     |
	| label    | Привіт знову!                            |
	| label    | Будь ласка введіть свої дані, щоб увійти |
	| input    | Електронна пошта                         |
	| input    | Пароль                                   |
	| checkbox | Запам’ятати мене                         |
	| link     | Забули пароль?                           |
	| button   | Увійти                                   |
	| link     | Зареєструватися                          |
  When I enter 'Not an email' in the input 'Електронна пошта'
  And I click the 'Пароль' input
  Then the input 'Електронна пошта' should have color 'rgb(255, 0, 0)'
  And the input 'Електронна пошта' should be invalid
  And the button 'Увійти' should be disabled
  When I enter the following data in the inputs
    | Field            | Value				   |
    | Електронна пошта | AppSettings[Login]    |
    | Пароль           | AppSettings[Password] |
  And I click the 'Увійти' button
  Then I should see the 'AppSettings[Username]' link
  
