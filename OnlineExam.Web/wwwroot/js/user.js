// User-specific functions
$(document).ready(function () {
    const path = window.location.pathname;

    if (path.includes('dashboard.html')) {
        loadUserDashboard();
    } else if (path.includes('quizzes.html')) {
        loadAvailableQuizzes();
    } else if (path.includes('take-quiz.html')) {
        const quizId = new URLSearchParams(window.location.search).get('id');
        loadQuizForTaking(quizId);
    } else if (path.includes('results.html')) {
        const resultId = new URLSearchParams(window.location.search).get('id');
        loadQuizResult(resultId);
    }
});

function loadUserDashboard() {
    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
    const userAttempts = db.results.filter(r => r.userId === currentUser.id);

    $('#welcomeMessage').text(`Welcome back, ${currentUser.username}!`);
    $('#totalAttempts').text(userAttempts.length);

    if (userAttempts.length > 0) {
        const averageScore = userAttempts.reduce((sum, attempt) => sum + attempt.score, 0) / userAttempts.length;
        $('#averageScore').text(`${averageScore.toFixed(1)}%`);

        // Recent attempts
        const recentAttempts = userAttempts.slice(0, 3).map(attempt => {
            const quiz = db.quizzes.find(q => q.id === attempt.quizId);
            return `
                <div class="list-group-item">
                    <div class="d-flex w-100 justify-content-between">
                        <h6 class="mb-1">${quiz.title}</h6>
                        <small>${new Date(attempt.completedAt).toLocaleDateString()}</small>
                    </div>
                    <p class="mb-1">Score: ${attempt.score}%</p>
                    <a href="results.html?id=${attempt.id}" class="btn btn-sm btn-outline-primary mt-2">
                        View Details
                    </a>
                </div>
            `;
        }).join('');

        $('#recentAttempts').html(recentAttempts);
    } else {
        $('#averageScore').text('N/A');
        $('#recentAttempts').html(`
            <div class="list-group-item">
                <p class="mb-1 text-muted">You haven't taken any quizzes yet.</p>
                <a href="quizzes.html" class="btn btn-sm btn-primary mt-2">
                    Take a Quiz
                </a>
            </div>
        `);
    }
}

function loadAvailableQuizzes() {
    const quizzes = db.quizzes.map(quiz => {
        return `
            <div class="col-md-6 col-lg-4 mb-4">
                <div class="card h-100">
                    <div class="card-body">
                        <h5 class="card-title">${quiz.title}</h5>
                        <p class="card-text">${quiz.description}</p>
                        <p class="text-muted"><small>${quiz.questions.length} questions</small></p>
                    </div>
                    <div class="card-footer bg-transparent">
                        <a href="take-quiz.html?id=${quiz.id}" class="btn btn-primary w-100">
                            Take Quiz
                        </a>
                    </div>
                </div>
            </div>
        `;
    }).join('');

    $('#quizzesContainer').html(quizzes);
}

function loadQuizForTaking(quizId) {
    const quiz = db.quizzes.find(q => q.id === parseInt(quizId));

    if (!quiz) {
        showAlert('Quiz not found', 'danger');
        setTimeout(() => {
            window.location.href = 'quizzes.html';
        }, 1500);
        return;
    }

    $('#quizTitle').text(quiz.title);
    $('#quizDescription').text(quiz.description);

    // Add questions to form
    quiz.questions.forEach((question, index) => {
        let optionsHtml = '';

        if (question.type === 'multiple-choice') {
            optionsHtml = question.options.map(option => `
                <div class="form-check mb-2">
                    <input class="form-check-input" type="radio" name="question-${question.id}" 
                           id="option-${option.id}" value="${option.id}">
                    <label class="form-check-label" for="option-${option.id}">
                        ${option.text}
                    </label>
                </div>
            `).join('');
        } else if (question.type === 'true-false') {
            optionsHtml = `
                <div class="form-check mb-2">
                    <input class="form-check-input" type="radio" name="question-${question.id}" 
                           id="option-true" value="true">
                    <label class="form-check-label" for="option-true">
                        True
                    </label>
                </div>
                <div class="form-check mb-2">
                    <input class="form-check-input" type="radio" name="question-${question.id}" 
                           id="option-false" value="false">
                    <label class="form-check-label" for="option-false">
                        False
                    </label>
                </div>
            `;
        }

        const questionHtml = `
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Question ${index + 1}</h5>
                </div>
                <div class="card-body">
                    <p class="card-text">${question.text}</p>
                    <div class="options-container mt-3">
                        ${optionsHtml}
                    </div>
                </div>
            </div>
        `;

        $('#quizQuestionsContainer').append(questionHtml);
    });

    // Form submission
    $('#quizForm').submit(function (e) {
        e.preventDefault();

        const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
        const answers = [];
        let correctCount = 0;

        quiz.questions.forEach(question => {
            const selectedOption = $(`input[name="question-${question.id}"]:checked`).val();

            if (selectedOption) {
                let isCorrect = false;

                if (question.type === 'multiple-choice') {
                    const selectedOptionObj = question.options.find(o => o.id === parseInt(selectedOption));
                    isCorrect = selectedOptionObj ? selectedOptionObj.correct : false;
                } else if (question.type === 'true-false') {
                    const correctAnswer = question.options.find(o => o.correct).text.toLowerCase();
                    isCorrect = selectedOption === correctAnswer;
                }

                if (isCorrect) correctCount++;

                answers.push({
                    questionId: question.id,
                    answerId: question.type === 'multiple-choice' ? parseInt(selectedOption) : selectedOption,
                    correct: isCorrect
                });
            }
        });

        const score = Math.round((correctCount / quiz.questions.length) * 100);

        // Save result
        const newResult = {
            id: db.results.length + 1,
            userId: currentUser.id,
            quizId: quiz.id,
            score: score,
            answers: answers,
            completedAt: new Date()
        };

        db.results.push(newResult);
        window.location.href = `results.html?id=${newResult.id}`;
    });
}

function loadQuizResult(resultId) {
    const result = db.results.find(r => r.id === parseInt(resultId));
    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));

    if (!result || result.userId !== currentUser.id) {
        showAlert('Result not found', 'danger');
        setTimeout(() => {
            window.location.href = 'dashboard.html';
        }, 1500);
        return;
    }

    const quiz = db.quizzes.find(q => q.id === result.quizId);

    $('#resultTitle').text(quiz.title);
    $('#resultScore').text(`${result.score}%`);
    $('#resultDate').text(new Date(result.completedAt).toLocaleString());

    // Progress bar
    const progressClass = result.score >= 80 ? 'bg-success' :
        result.score >= 50 ? 'bg-warning' : 'bg-danger';
    $('#resultProgressBar')
        .css('width', `${result.score}%`)
        .addClass(progressClass);

    // Question details
    let detailsHtml = '';

    result.answers.forEach(answer => {
        const question = quiz.questions.find(q => q.id === answer.questionId);
        const isCorrect = answer.correct ? 'text-success' : 'text-danger';
        const icon = answer.correct ? 'fa-check' : 'fa-times';

        let userAnswerText = '';
        if (question.type === 'multiple-choice') {
            const selectedOption = question.options.find(o => o.id === answer.answerId);
            userAnswerText = selectedOption ? selectedOption.text : 'No answer';
        } else {
            userAnswerText = answer.answerId === 'true' ? 'True' : 'False';
        }

        detailsHtml += `
            <div class="card mb-3">
                <div class="card-header ${isCorrect}">
                    <i class="fas ${icon} me-2"></i>
                    <strong>Question:</strong> ${question.text}
                </div>
                <div class="card-body">
                    <p><strong>Your answer:</strong> ${userAnswerText}</p>
                    ${answer.correct ? '' : `
                        <p class="text-success"><strong>Correct answer:</strong> ${question.options.find(o => o.correct).text
                }</p>
                    `}
                </div>
            </div>
        `;
    });

    $('#resultDetails').html(detailsHtml);
}