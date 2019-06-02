/// <reference types="Cypress" />

context('UserSearch', () => {
    beforeEach(() => {
        cy.visit('http://localhost:52907/');
    });

    describe('searching for a user that exists and has repositories', () => {
        it('should show user and repositories', () => {
            cy.get('#search-box').type('cypress-io');
            cy.get('#search-form').submit();

            cy.get('#error').should('not.be.visible');

            cy.get('#user-name').should('have.text', 'Cypress.io');
            cy.get('#repositories .card').should('have.length', 5);
            cy.get('#repositories .card:nth-child(1) .card-title').should('have.text', 'cypress');
            cy.get('#repositories .card:nth-child(2) .card-title').should('have.text', 'cypress-docker-images');
            cy.get('#repositories .card:nth-child(3) .card-title').should('have.text', 'cypress-documentation');
            cy.get('#repositories .card:nth-child(4) .card-title').should('have.text', 'circleci-orb');
            cy.get('#repositories .card:nth-child(5) .card-title').should('have.text', 'cypress-cli');
        });
    });

    describe('searching for a user that doesn\'t exist', () => {
        it('should say the user cannot be found', () => {
            cy.get('#search-box').type('sdfkdjshfgkdjsfgsdf');
            cy.get('#search-form').submit();

            cy.get('#error').should('be.visible').and('have.text', 'User not found!');
        });
    });

    describe('searching for a user that exists and has no repositories', () => {
        it('should show the user and a message indicating they have no repositories', () => {
            cy.get('#search-box').type('asd');
            cy.get('#search-form').submit();

            cy.get('#error').should('not.be.visible');

            cy.get('#user-name').should('have.text', 'ASD');
            cy.get('#repositories').should('have.text', 'This user has no public repositories!');
        });
    });
});