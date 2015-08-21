function isFirstNameEmpty() {
    if(document.getElementById("txtFName").value == ""){
        return "First Name should not be empty";
    }
    else {
        return "";
    }
}

function isFirstNameInvalid() {
    if (document.getElementById("txtFName").value.indexOf("@") != -1) {
        return "First Name should not contains @";
    }
    else {
        return "";
    }
}

function isLastNameInvalid() {
    if (document.getElementById("txtLName").value.length > 5) {
        return "Last Name should not contains more than 5 characters";
    }
    else { return "";}
}

function isSalaryEmpty() {
    if (document.getElementById("txtSalary").value == "") {
        return "Salary should not be empty";
    }
    else { return "";}
}

function isSalaryInvalid() {
    if (isNaN(document.getElementById("txtSalary").value)) {
        return "Salary should be a positive whole number";
    }
    else { return "";}
}

function isValid() {
    var FirstNameEmptyMessage = isFirstNameEmpty();
    var FirstNameInValidMessage = isFirstNameInvalid();
    var LastNameInValidMessage = isLastNameInvalid();
    var SalaryEmptyMessage = isSalaryEmpty();
    var SalaryInvalidMessage = isSalaryInvalid();

    var FinalErrorMessage = "Errors:";

    if (FirstNameEmptyMessage != "")
        FinalErrorMessage += FirstNameEmptyMessage;
    if (FirstNameInValidMessage != "")
        FinalErrorMessage += FirstNameInValidMessage;
    if (LastNameInValidMessage != "")
        FinalErrorMessage += LastNameInValidMessage;
    if (SalaryEmptyMessage != "")
        FinalErrorMessage += SalaryEmptyMessage;
    if (SalaryInvalidMessage != "")
        FinalErrorMessage += SalaryInvalidMessage;

    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        
        return true;
    }

}