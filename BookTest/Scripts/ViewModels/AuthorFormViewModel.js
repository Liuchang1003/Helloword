function AuthorFormViewModel() {
    var self = this;
    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);
    self.isCreate = author.id == 0;

    self.author = {
        id:author.id,
        firstName: ko.observable(),
        lastName: ko.observable(),
        biography: ko.observable()
    };

    self.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;
        self.sending(true);

        self.author.__RequestVerificationToken = form[0].value;

        $.ajax({
                url: isCreate?'Create':'Edit',
                type: 'post',
                contentType: 'application/x-www-form-urlencoded',
                data: ko.toJS(self.author)
            })
            .success(self.successfulSave)
            .error(self.errorSave)
            .complete(function() { self.seeding(false) });
    }

    self.successfulSave = function() {
        self.saveCompleted(true);
        $('.body-content').prepend('<div class="alert alert-success"> <strong>Success!</strong>The mew author has been saved.</div>');
        setTimeout(function () {
            if (self.isCreate)
                location.href = './';
            else 
                location.href = '../';
        }, 5000);
    };
    self.errorSave = function() {
        $('.body-content').prepend('<div class="alert alert-danger"> <strong>Error!</strong>There was an error creating the author.</div>');
    }
}