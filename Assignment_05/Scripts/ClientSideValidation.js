// AJAX for Teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

	//check for validation straight away
	var IsValid = ValidateAuthor();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:56313/api/TeacherData/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:56313/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}
// Usually Validation functions for Add and Update are separated.
// You can run into situations where information added is no longer updated, or vice versa
// However, as an example, validation is consolidated into 'ValidateTeacher'

function ValidateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;


	//First Name is two or more characters
	if (TeacherFname.length < 2) {
		IsValid = false;
		ErrorMsg += "First Name Must be 2 or more characters.<br>";
	}
	//Last Name is two or more characters
	if (TeacherLname.length < 2) {
		IsValid = false;
		ErrorMsg += "Last Name Must be 2 or more characters.<br>";
	}
	//EmployeeNumber is valid pattern
	if (EmployeeNumber.length >= 4) {
		IsValid = false;
		ErrorMsg += "Please Enter a valid Employee Number.<br>";
	}

	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}


	return IsValid;
}





function UpdateTeacher(TeacherId) {

	//check for validation straight away
	var IsValid = ValidateAuthor();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:56313/api/TeacherData/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:56313/api/TeacherData/AddTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}