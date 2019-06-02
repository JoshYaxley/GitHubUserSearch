import './index.less';
import 'bootstrap';

interface User {
    Login: string;
    AvatarUrl: string;
    RepositoriesUrl: string;
    Name: string;
    Location: string;
}

interface Repository {
    Name: string;
    Description: string;
    Language: string;
    StargazersCount: number;
}

$(() => {
    $('#search-form').submit(e => {
        e.preventDefault();

        const searchBoxContents = (<any>$('#search-box')).val();
        if (searchBoxContents) {
            const userName = searchBoxContents.trim();

            const repositoriesContainer = $('#repositories');
            repositoriesContainer.empty();

            loadUser(userName);
        }
    });
});

function loadUser(userName: string) {
    const button = $('#search-button');
    button.prop('disabled', true);

    const url = '/api/userByUserName?userName=' + userName;
    $.ajax(url).done((user: User) => {
        $('#user').removeClass('d-none');
        $('#user-avatar').attr('src', user.AvatarUrl);
        $('#user-name').text(user.Name || user.Login);
        $('#user-location').text(user.Location || 'No location set.');

        showContent();

        loadRepositories(userName);
    }).fail(error => {
        if (error.status === 404) {
            showError('User not found!');
        } else {
            showError('Something went wrong!');
        }
    }).always(() => {
        button.prop('disabled', false);
    });
}

function loadRepositories(userName: string) {
    const repositoriesContainer = $('#repositories');

    const url = '/api/repositoriesByUserName?userName=' + userName;

    $.ajax(url).done((repositories: Repository[]) => {
        if (repositories.length === 0) {
            repositoriesContainer.append("<div class='text-center mt-3'>This user has no public repositories!</div>");
            return;
        }

        const repositoryTemplate = $('#repository-template');

        repositories
            .sort((x: Repository, y: Repository) => y.StargazersCount - x.StargazersCount)
            .slice(0, 5)
            .forEach((repository: Repository) => {
                const element = repositoryTemplate.clone();

                element.removeAttr('id');
                element.find('.card-title').text(repository.Name);
                element.find('.card-text').text(repository.Description);
                element.find('.repository-language').text(repository.Language);
                element.find('.repository-stars').text(repository.StargazersCount.toString() + ' Star' + (repository.StargazersCount === 1 ? '' : 's'));
                element.removeClass('d-none');

                repositoriesContainer.append(element);
            });
    }).fail(_ => {
        showError('Something went wrong!');
    });
}

function showContent() {
    $('#content').removeClass('d-none');
    hideError();
}

function showError(errorMessage: string) {
    $('#content').addClass('d-none');

    const errorElement = $('#error');
    errorElement.text(errorMessage);
    errorElement.removeClass('d-none');
}

function hideError() {
    $('#error').addClass('d-none');
}