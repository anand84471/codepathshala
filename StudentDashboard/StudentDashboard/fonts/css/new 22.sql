INSERT [dbo].[AGGREGATOR_BANK_OTP_URLS_TBL] ([BANK_NAME], [BANK_URL], [ROW_INSERTION_DATETIME], [ROW_UPDATION_DATETIME], [ROW_ACTION_COUNT], [IS_PAGE_TO_BE_RESPONSIVE], [IS_OTP_URL], [IS_PAGE_NETBANKING_SUBMISSION_PAGE], [PAGE_CUSTOMIZATION_JS_CODE], [NET_BANKING_PAGE_SUBMISSIOON_JS_CODE], [OTP_LENGTH], [JS_EXECUTION_ERROR], [JS_ERROR_DATETIME], [IS_JS_ERROR_OCCURRED], [PAGE_CUSTOMIZATION_JS_CODE_FOR_IOS], [OTP_PAGE_SUBMISSION_JS_CODE], [IS_NETBANKING_LOGIN_PAGE], [JS_CODE_TO_GET_CUSTOMER_ID], [IS_NETBANKING_PAGE], [JS_CODE_TO_SET_CUSTOMER_ID], [IS_AUTO_OTP_SUBMIT_DISABLED], [IS_GENERIC_OTP_FILLING_JS_CODE_DISABLED], [JS_CODE_TO_GET_NETBANKING_USER_ID_FROM_PAGE_IOS], [JS_CODE_TO_SET_NETBANKING_USER_ID_TO_PAGE_IOS], [IS_OTP_PAGE_CHECK_ENABLED], [OTP_PAGE_CHECK_JS_CODE]) VALUES (N'OTP_PAGE_SBI_CREDIT_CARD', N'https://secure4.arcot.com/acspage/cap?RID=41907&VAA=B', CAST(N'2020-04-13T00:00:00.000' AS DateTime), CAST(N'2020-04-13T00:00:00.000' AS DateTime), 0, 1, 1, 0, N'var submitButton=$("#sendotp");var x=$("#pwdenterPIN");x.hide();var otpBox=$("#otpContent");var info=$(".form-content");otpBox.insertAfter(info);var x=document.getElementById("enterPIN");x.style.width="100%";x.style.marginTop="10px";x.style.marginBottom="10px";var otpInformation=$("#pwdsubheader");var x=document.getElementById("pwdsubheader");x.style.textAlign="center";var x=document.getElementById("pwdheader");x.style.textAlign="center";var x=document.getElementById("sendotp");x.style.width="100%";x.style.marginLeft="2px";x.style.marginBottom="10px";submitButton.insertBefore(otpInformation);var otpMessage=$("#pwdheader");otpMessage.insertAfter(info);', N'', 6, NULL, NULL, NULL, N'var submitButton=$("#sendotp");var x=$("#pwdenterPIN");x.hide();var otpBox=$("#otpContent");var info=$(".form-content");otpBox.insertAfter(info);var x=document.getElementById("enterPIN");x.style.width="100%";x.style.marginTop="10px";x.style.marginBottom="10px";var otpInformation=$("#pwdsubheader");var x=document.getElementById("pwdsubheader");x.style.textAlign="center";var x=document.getElementById("pwdheader");x.style.textAlign="center";var x=document.getElementById("sendotp");x.style.width="100%";x.style.marginLeft="2px";x.style.marginBottom="10px";submitButton.insertBefore(otpInformation);var otpMessage=$("#pwdheader");otpMessage.insertAfter(info);', NULL, 0, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)