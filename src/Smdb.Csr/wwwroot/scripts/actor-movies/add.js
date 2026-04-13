import { $, apiFetch, renderStatus, captureActorMovieForm } from '/scripts/common.js';

(async function initActorMovieAdd() {
	const form = $('#am-form');
	const statusEl = $('#status');
	const actorSelect = $('#actorId');
	const movieSelect = $('#movieId');

	// Populate actor and movie dropdowns
	try {
		const [actorsPayload, moviesPayload] = await Promise.all([
			apiFetch('/actors?page=1&size=100'),
			apiFetch('/movies?page=1&size=100'),
		]);
		const actors = Array.isArray(actorsPayload) ? actorsPayload : (actorsPayload.data || []);
		const movies = Array.isArray(moviesPayload) ? moviesPayload : (moviesPayload.data || []);

		for (const a of actors) {
			const opt = document.createElement('option');
			opt.value = a.id;
			opt.textContent = `#${a.id} – ${a.name}`;
			actorSelect.appendChild(opt);
		}
		for (const m of movies) {
			const opt = document.createElement('option');
			opt.value = m.id;
			opt.textContent = `#${m.id} – ${m.title} (${m.year})`;
			movieSelect.appendChild(opt);
		}
		renderStatus(statusEl, 'ok', 'Select an actor and a movie to link.');
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to load options: ${err.message}`);
		form.querySelectorAll('select,button').forEach(el => el.disabled = true);
		return;
	}

	form.addEventListener('submit', async (ev) => {
		ev.preventDefault();
		const payload = captureActorMovieForm(form);
		try {
			const created = await apiFetch('/actors-movies', { method: 'POST', body: JSON.stringify(payload) });
			renderStatus(statusEl, 'ok', `Created link #${created.id} (Actor ${created.actorId} → Movie ${created.movieId}).`);
			form.reset();
		} catch (err) {
			renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
		}
	});
})();
